using System;
using System.Collections.Generic;

namespace PatientPortal.Application.DTOs
{
    public class PatientProfileDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PreferredLanguage { get; set; }
        public bool AccessibilityModeEnabled { get; set; }
        public bool MultiFactorEnabled { get; set; }
        public bool BiometricEnrollmentComplete { get; set; }
        public IReadOnlyCollection<CommunicationPreferenceDto> CommunicationPreferences { get; set; }
        public IReadOnlyCollection<DeviceIntegrationDto> Devices { get; set; }
        public InsuranceDto Insurance { get; set; }
    }
}
