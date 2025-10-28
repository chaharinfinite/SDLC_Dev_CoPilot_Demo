using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class DeviceIntegrationService : IDeviceIntegrationService
    {
        private readonly IRepository<DeviceIntegration> _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceIntegrationService(IRepository<DeviceIntegration> deviceRepository, IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<DeviceIntegrationDto>> GetDevicesAsync(string patientUserId)
        {
            var devices = await _deviceRepository.SearchAsync(device => device.PatientUserId == patientUserId);
            return devices.Select(Map).ToList();
        }

        public async Task RegisterDeviceAsync(DeviceIntegrationDto device)
        {
            var entity = new DeviceIntegration(device.DeviceId, device.DeviceType, device.ProviderName, device.PatientUserId);
            await _deviceRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RecordSyncAsync(string deviceId)
        {
            var matches = await _deviceRepository.SearchAsync(device => device.DeviceId == deviceId);
            var device = matches.FirstOrDefault();
            if (device == null)
            {
                return;
            }

            device.RecordSync();
            await _deviceRepository.UpdateAsync(device);
            await _unitOfWork.SaveChangesAsync();
        }

        private static DeviceIntegrationDto Map(DeviceIntegration device)
        {
            return new DeviceIntegrationDto
            {
                DeviceId = device.DeviceId,
                DeviceType = device.DeviceType,
                ProviderName = device.ProviderName,
                PatientUserId = device.PatientUserId,
                LinkedOn = device.LinkedOn,
                LastSync = device.LastSync
            };
        }
    }
}
