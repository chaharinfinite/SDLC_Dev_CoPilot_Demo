using System;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Infrastructure.Notifications
{
    public class ConsoleNotificationGateway : INotificationGateway
    {
        public Task DispatchAsync(NotificationDto notification)
        {
            Console.WriteLine($"[{notification.Channel}] {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
