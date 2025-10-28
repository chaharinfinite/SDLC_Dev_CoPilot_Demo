using System;
using System.Collections.Generic;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Application.DTOs
{
    public class MedicalRecordDetailDto
    {
        public Guid Id { get; set; }
        public RecordType RecordType { get; set; }
        public string Summary { get; set; }
        public string DocumentUri { get; set; }
        public DateTimeOffset AvailableOn { get; set; }
        public IReadOnlyCollection<HistoricalTrendPoint> Trends { get; set; }
    }
}
