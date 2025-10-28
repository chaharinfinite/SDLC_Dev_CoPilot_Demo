using System;
using System.Collections.Generic;

namespace PatientPortal.Application.DTOs
{
    public class PatientProfileUpdateRequest
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public InsuranceDto Insurance { get; set; }
        public bool EnableMultiFactor { get; set; }
        public bool EnableBiometrics { get; set; }
        public string PreferredLanguage { get; set; }
        public IReadOnlyCollection<CommunicationPreferenceDto> CommunicationPreferences { get; set; }
    }
}
