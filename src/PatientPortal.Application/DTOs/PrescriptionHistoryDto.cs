using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class PrescriptionHistoryDto
    {
        public Guid PrescriptionId { get; set; }
        public PrescriptionStatus Status { get; set; }
        public DateTimeOffset ChangedOn { get; set; }
        public string Notes { get; set; }
    }
}
