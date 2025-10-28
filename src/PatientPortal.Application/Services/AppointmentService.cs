using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IRepository<Appointment> appointmentRepository, INotificationService notificationService, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AppointmentDto>> GetUpcomingAppointmentsAsync(string patientUserId)
        {
            var appointments = await _appointmentRepository.SearchAsync(appointment => appointment.PatientUserId == patientUserId && appointment.StartTime >= DateTimeOffset.UtcNow.AddDays(-1));
            return appointments
                .OrderBy(appointment => appointment.StartTime)
                .Select(Map)
                .ToList();
        }

        public async Task<AppointmentDto> ScheduleAppointmentAsync(AppointmentRequest request)
        {
            var clashes = await _appointmentRepository.SearchAsync(appointment =>
                appointment.PatientUserId == request.PatientUserId &&
                appointment.StartTime < request.DesiredStart + request.Duration &&
                request.DesiredStart < appointment.StartTime + appointment.Duration);

            if (clashes.Any())
            {
                throw new InvalidOperationException("Overlapping appointment detected");
            }

            var appointment = new Appointment(request.PatientUserId, request.ProviderId, request.DesiredStart, request.Duration, request.Location);
            appointment.UpdateReason(request.ReasonForVisit);

            await _appointmentRepository.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            await _notificationService.ScheduleReminderAsync(new NotificationRequest
            {
                PatientUserId = request.PatientUserId,
                Message = $"Appointment confirmed for {appointment.StartTime:G}",
                Channel = Domain.Enums.NotificationChannel.Email,
                ScheduledOn = appointment.StartTime.AddHours(-24),
                RequiresAcknowledgement = false
            });

            return Map(appointment);
        }

        public async Task RescheduleAppointmentAsync(Guid appointmentId, AppointmentRescheduleRequest request)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found");
            }

            appointment.Reschedule(request.NewStartTime, request.Duration, request.UpdatedBy);
            await _appointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelAppointmentAsync(Guid appointmentId, string cancelledBy)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found");
            }

            appointment.Cancel(cancelledBy);
            await _appointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found");
            }

            appointment.Confirm();
            await _appointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        private static AppointmentDto Map(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                PatientUserId = appointment.PatientUserId,
                ProviderId = appointment.ProviderId,
                StartTime = appointment.StartTime,
                Duration = appointment.Duration,
                Location = appointment.Location,
                Status = appointment.Status,
                ReasonForVisit = appointment.ReasonForVisit,
                Notes = appointment.Notes
            };
        }
    }
}
