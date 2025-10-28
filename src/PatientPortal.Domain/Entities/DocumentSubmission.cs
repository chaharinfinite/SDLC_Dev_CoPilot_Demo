using System;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class DocumentSubmission : EntityBase
    {
        private DocumentSubmission()
        {
        }

        public DocumentSubmission(string patientUserId, string documentType, string fileUri)
        {
            PatientUserId = patientUserId;
            DocumentType = documentType;
            FileUri = fileUri;
        }

        public string PatientUserId { get; private set; }
        public string DocumentType { get; private set; }
        public string FileUri { get; private set; }
        public DateTimeOffset SubmittedOn { get; private set; } = DateTimeOffset.UtcNow;
        public bool Verified { get; private set; }

        public void MarkVerified()
        {
            Verified = true;
        }
    }
}
