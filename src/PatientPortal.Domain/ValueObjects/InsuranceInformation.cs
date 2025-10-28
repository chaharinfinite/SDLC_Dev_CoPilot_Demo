using System;

namespace PatientPortal.Domain.ValueObjects
{
    public sealed class InsuranceInformation
    {
        public InsuranceInformation(string providerName, string policyNumber, DateTime? effectiveDate, DateTime? expirationDate)
        {
            ProviderName = providerName?.Trim();
            PolicyNumber = policyNumber?.Trim();
            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
        }

        public string ProviderName { get; }
        public string PolicyNumber { get; }
        public DateTime? EffectiveDate { get; }
        public DateTime? ExpirationDate { get; }
        public string GroupNumber { get; private set; }
        public string PlanType { get; private set; }

        public InsuranceInformation WithGroup(string groupNumber, string planType)
        {
            var copy = new InsuranceInformation(ProviderName, PolicyNumber, EffectiveDate, ExpirationDate)
            {
                GroupNumber = groupNumber,
                PlanType = planType
            };

            return copy;
        }
    }
}
