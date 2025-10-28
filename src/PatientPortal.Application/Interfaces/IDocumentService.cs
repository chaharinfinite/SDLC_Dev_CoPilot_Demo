using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<IReadOnlyList<PatientDocumentDto>> GetDocumentsAsync(string patientUserId);
        Task UploadDocumentAsync(DocumentUploadRequest request);
        Task MarkVerifiedAsync(Guid documentId);
    }
}
