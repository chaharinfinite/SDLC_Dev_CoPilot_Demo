using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<DocumentSubmission> _documentRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(
            IRepository<DocumentSubmission> documentRepository,
            IFileStorageService fileStorageService,
            IUnitOfWork unitOfWork)
        {
            _documentRepository = documentRepository;
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<PatientDocumentDto>> GetDocumentsAsync(string patientUserId)
        {
            var documents = await _documentRepository.SearchAsync(document => document.PatientUserId == patientUserId);
            return documents
                .OrderByDescending(document => document.SubmittedOn)
                .Select(document => new PatientDocumentDto
                {
                    Id = document.Id,
                    DocumentType = document.DocumentType,
                    FileUri = document.FileUri,
                    SubmittedOn = document.SubmittedOn,
                    Verified = document.Verified
                })
                .ToList();
        }

        public async Task UploadDocumentAsync(DocumentUploadRequest request)
        {
            var storageUri = await _fileStorageService.UploadAsync(request.FileName, request.Content, request.ContentType);
            var submission = new DocumentSubmission(request.PatientUserId, request.DocumentType, storageUri);
            await _documentRepository.AddAsync(submission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MarkVerifiedAsync(Guid documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null)
            {
                throw new InvalidOperationException("Document not found");
            }

            document.MarkVerified();
            await _documentRepository.UpdateAsync(document);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
