using System;

namespace PatientPortal.Application.DTOs
{
    public class DeviceIntegrationDto
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string ProviderName { get; set; }
        public string PatientUserId { get; set; }
        public DateTimeOffset LinkedOn { get; set; }
        public DateTimeOffset? LastSync { get; set; }
    }
}
