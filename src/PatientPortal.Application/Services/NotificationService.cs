using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<PortalNotification> _notificationRepository;
        private readonly INotificationGateway _notificationGateway;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(
            IRepository<PortalNotification> notificationRepository,
            INotificationGateway notificationGateway,
            IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _notificationGateway = notificationGateway;
            _unitOfWork = unitOfWork;
        }

        public async Task ScheduleReminderAsync(NotificationRequest request)
        {
            var scheduledOn = request.ScheduledOn ?? DateTimeOffset.UtcNow;
            var notification = new PortalNotification(request.PatientUserId, request.Message, request.Channel, scheduledOn);
            if (request.RequiresAcknowledgement)
            {
                notification.RequireAcknowledgement();
            }

            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            if (scheduledOn <= DateTimeOffset.UtcNow)
            {
                await DispatchAsync(notification);
            }
        }

        public async Task<IReadOnlyList<NotificationDto>> GetPendingNotificationsAsync(string patientUserId)
        {
            var notifications = await _notificationRepository.SearchAsync(notification => notification.PatientUserId == patientUserId && notification.DeliveredOn == null);
            return notifications.Select(Map).ToList();
        }

        public async Task MarkDeliveredAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification == null)
            {
                throw new InvalidOperationException("Notification not found");
            }

            notification.MarkDelivered();
            await _notificationRepository.UpdateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task DispatchAsync(PortalNotification notification)
        {
            await _notificationGateway.DispatchAsync(Map(notification));
            notification.MarkDelivered();
            await _notificationRepository.UpdateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        private static NotificationDto Map(PortalNotification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                Message = notification.Message,
                Channel = notification.Channel,
                ScheduledOn = notification.ScheduledOn,
                DeliveredOn = notification.DeliveredOn,
                RequiresAcknowledgement = notification.RequiresAcknowledgement
            };
        }
    }
}
