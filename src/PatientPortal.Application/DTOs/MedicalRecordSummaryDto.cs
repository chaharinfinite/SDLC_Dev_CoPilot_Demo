using System;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class MedicalRecordSummaryDto
    {
        public Guid Id { get; set; }
        public RecordType RecordType { get; set; }
        public string Summary { get; set; }
        public DateTimeOffset AvailableOn { get; set; }
        public bool IsDownloadable { get; set; }
    }
}
