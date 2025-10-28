using System;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.Entities
{
    public class Prescription : EntityBase
    {
        private Prescription()
        {
        }

        public Prescription(string patientUserId, string medicationName, string dosage, string providerId)
        {
            PatientUserId = patientUserId;
            MedicationName = medicationName;
            DosageInstructions = dosage;
            ProviderId = providerId;
            Status = PrescriptionStatus.Active;
        }

        public string PatientUserId { get; private set; }
        public string MedicationName { get; private set; }
        public string DosageInstructions { get; private set; }
        public string ProviderId { get; private set; }
        public PrescriptionStatus Status { get; private set; }
        public DateTimeOffset? LastRefillOn { get; private set; }
        public DateTimeOffset? NextRefillAvailableOn { get; private set; }
        public bool AllowAutoRefill { get; private set; }

        public void RequestRefill()
        {
            Status = PrescriptionStatus.RefillRequested;
        }

        public void ApproveRefill(DateTimeOffset nextAvailable)
        {
            Status = PrescriptionStatus.Fulfilled;
            LastRefillOn = DateTimeOffset.UtcNow;
            NextRefillAvailableOn = nextAvailable;
        }

        public void RejectRefill()
        {
            Status = PrescriptionStatus.Rejected;
        }

        public void Expire()
        {
            Status = PrescriptionStatus.Expired;
        }

        public void SetAutoRefill(bool enabled)
        {
            AllowAutoRefill = enabled;
        }
    }
}
