using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface INotificationGateway
    {
        Task DispatchAsync(NotificationDto notification);
    }
}
