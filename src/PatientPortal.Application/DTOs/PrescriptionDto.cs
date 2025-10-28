using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }
        public string MedicationName { get; set; }
        public string DosageInstructions { get; set; }
        public PrescriptionStatus Status { get; set; }
        public DateTimeOffset? LastRefillOn { get; set; }
        public DateTimeOffset? NextRefillAvailableOn { get; set; }
        public bool AllowAutoRefill { get; set; }
    }
}
