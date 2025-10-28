using System;

namespace PatientPortal.Application.DTOs
{
    public class PatientDocumentDto
    {
        public Guid Id { get; set; }
        public string DocumentType { get; set; }
        public string FileUri { get; set; }
        public DateTimeOffset SubmittedOn { get; set; }
        public bool Verified { get; set; }
    }
}
