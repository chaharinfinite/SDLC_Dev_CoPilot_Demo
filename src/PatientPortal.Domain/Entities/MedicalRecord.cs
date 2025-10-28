using System;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.Enums;

namespace PatientPortal.Domain.Entities
{
    public class MedicalRecord : EntityBase
    {
        private MedicalRecord()
        {
        }

        public MedicalRecord(string patientUserId, RecordType recordType, string documentUri, string summary)
        {
            PatientUserId = patientUserId;
            RecordType = recordType;
            DocumentUri = documentUri;
            Summary = summary;
        }

        public string PatientUserId { get; private set; }
        public RecordType RecordType { get; private set; }
        public string DocumentUri { get; private set; }
        public string Summary { get; private set; }
        public DateTimeOffset AvailableOn { get; private set; } = DateTimeOffset.UtcNow;
        public bool IsDownloadable { get; private set; } = true;
        public string Hash { get; private set; }

        public void SetHash(string hash)
        {
            Hash = hash;
        }
    }
}
