using System;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class TelehealthService : ITelehealthService
    {
        private readonly IRepository<TelehealthSession> _telehealthRepository;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public TelehealthService(
            IRepository<TelehealthSession> telehealthRepository,
            INotificationService notificationService,
            IUnitOfWork unitOfWork)
        {
            _telehealthRepository = telehealthRepository;
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TelehealthSessionDto> CreateSessionAsync(TelehealthSessionRequest request)
        {
            var session = new TelehealthSession(request.AppointmentId, request.PatientUserId, request.MeetingUrl);
            session.SetWaitingRoom(request.WaitingRoomUrl);
            session.LinkMonitoringDevice(request.MonitoringDeviceId);

            await _telehealthRepository.AddAsync(session);
            await _unitOfWork.SaveChangesAsync();

            await _notificationService.ScheduleReminderAsync(new NotificationRequest
            {
                PatientUserId = request.PatientUserId,
                Message = "Telehealth session starting soon",
                Channel = Domain.Enums.NotificationChannel.Push,
                ScheduledOn = DateTimeOffset.UtcNow.AddMinutes(5),
                RequiresAcknowledgement = false
            });

            return Map(session);
        }

        public async Task StartSessionAsync(Guid sessionId)
        {
            var session = await _telehealthRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Telehealth session not found");
            }

            session.StartSession();
            await _telehealthRepository.UpdateAsync(session);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CompleteSessionAsync(Guid sessionId)
        {
            var session = await _telehealthRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Telehealth session not found");
            }

            session.EndSession();
            await _telehealthRepository.UpdateAsync(session);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task FlagTechnicalIssueAsync(Guid sessionId, string description)
        {
            var session = await _telehealthRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Telehealth session not found");
            }

            session.FlagTechnicalIssue();
            await _telehealthRepository.UpdateAsync(session);
            await _unitOfWork.SaveChangesAsync();

            await _notificationService.ScheduleReminderAsync(new NotificationRequest
            {
                PatientUserId = session.PatientUserId,
                Message = $"Telehealth technical issue reported: {description}",
                Channel = Domain.Enums.NotificationChannel.Email,
                ScheduledOn = DateTimeOffset.UtcNow,
                RequiresAcknowledgement = true
            });
        }

        private static TelehealthSessionDto Map(TelehealthSession session)
        {
            return new TelehealthSessionDto
            {
                Id = session.Id,
                AppointmentId = session.AppointmentId,
                PatientUserId = session.PatientUserId,
                MeetingUrl = session.MeetingUrl,
                VirtualWaitingRoomUrl = session.VirtualWaitingRoomUrl,
                Status = session.Status,
                StartedOn = session.StartedOn,
                EndedOn = session.EndedOn,
                MonitoringDeviceId = session.MonitoringDeviceId
            };
        }
    }
}
