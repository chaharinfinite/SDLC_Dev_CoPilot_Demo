using System;

namespace PatientPortal.Application.DTOs
{
    public class DocumentUploadRequest
    {
        public string PatientUserId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
