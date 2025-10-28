using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface INotificationService
    {
        Task ScheduleReminderAsync(NotificationRequest request);
        Task<IReadOnlyList<NotificationDto>> GetPendingNotificationsAsync(string patientUserId);
        Task MarkDeliveredAsync(Guid notificationId);
    }
}
