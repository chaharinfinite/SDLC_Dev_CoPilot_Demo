using System;

namespace PatientPortal.Application.DTOs
{
    public class HistoricalTrendPoint
    {
        public DateTimeOffset Timestamp { get; set; }
        public string MetricName { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }
}
