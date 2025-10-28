using System;
using System.Collections.Generic;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class LabResult : EntityBase
    {
        private readonly List<LabResultMetric> _metrics = new List<LabResultMetric>();

        private LabResult()
        {
        }

        public LabResult(string patientUserId, string orderNumber, string documentUri)
        {
            PatientUserId = patientUserId;
            OrderNumber = orderNumber;
            DocumentUri = documentUri;
        }

        public string PatientUserId { get; private set; }
        public string OrderNumber { get; private set; }
        public string DocumentUri { get; private set; }
        public DateTimeOffset ReleasedOn { get; private set; } = DateTimeOffset.UtcNow;

        public IReadOnlyCollection<LabResultMetric> Metrics => _metrics.AsReadOnly();

        public void AddMetric(string name, string value, string unit, string referenceRange)
        {
            _metrics.Add(new LabResultMetric(name, value, unit, referenceRange));
        }
    }
}
