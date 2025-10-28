using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IDeviceIntegrationService
    {
        Task<IReadOnlyList<DeviceIntegrationDto>> GetDevicesAsync(string patientUserId);
        Task RegisterDeviceAsync(DeviceIntegrationDto device);
        Task RecordSyncAsync(string deviceId);
    }
}
