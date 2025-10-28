using System;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IPatientProfileService
    {
        Task<PatientProfileDto> GetProfileAsync(string userId);
        Task UpdateProfileAsync(PatientProfileUpdateRequest request);
        Task EnableAccessibilityAsync(string userId, bool enabled);
        Task SetCommunicationPreferenceAsync(string userId, CommunicationPreferenceDto preference);
        Task RegisterDeviceAsync(string userId, DeviceIntegrationDto device);
    }
}
