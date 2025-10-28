using System;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class DeviceIntegration : EntityBase
    {
        private DeviceIntegration()
        {
        }

        public DeviceIntegration(string deviceId, string deviceType, string providerName, string patientUserId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentException("Device id is required", nameof(deviceId));
            }

            DeviceId = deviceId;
            DeviceType = deviceType;
            ProviderName = providerName;
            PatientUserId = patientUserId;
        }

        public string DeviceId { get; private set; }
        public string DeviceType { get; private set; }
        public string ProviderName { get; private set; }
        public string PatientUserId { get; private set; }
        public DateTimeOffset LinkedOn { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastSync { get; private set; }

        public void RecordSync()
        {
            LastSync = DateTimeOffset.UtcNow;
        }
    }
}
