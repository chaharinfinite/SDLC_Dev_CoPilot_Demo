using System;

namespace PatientPortal.Application.DTOs
{
    public class InsuranceDto
    {
        public string ProviderName { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
        public string PlanType { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
