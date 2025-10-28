using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IRepository<Prescription> _prescriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionService(IRepository<Prescription> prescriptionRepository, IUnitOfWork unitOfWork)
        {
            _prescriptionRepository = prescriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<PrescriptionDto>> GetActivePrescriptionsAsync(string patientUserId)
        {
            var prescriptions = await _prescriptionRepository.SearchAsync(prescription => prescription.PatientUserId == patientUserId);
            return prescriptions
                .Where(prescription => prescription.Status != Domain.Enums.PrescriptionStatus.Expired)
                .Select(MapToDto)
                .ToList();
        }

        public async Task RequestRefillAsync(Guid prescriptionId)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(prescriptionId);
            if (prescription == null)
            {
                throw new InvalidOperationException("Prescription not found");
            }

            prescription.RequestRefill();
            await _prescriptionRepository.UpdateAsync(prescription);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<PrescriptionHistoryDto>> GetHistoryAsync(string patientUserId)
        {
            var prescriptions = await _prescriptionRepository.SearchAsync(prescription => prescription.PatientUserId == patientUserId);
            return prescriptions
                .OrderByDescending(prescription => prescription.UpdatedOn ?? prescription.CreatedOn)
                .Select(prescription => new PrescriptionHistoryDto
                {
                    PrescriptionId = prescription.Id,
                    Status = prescription.Status,
                    ChangedOn = prescription.UpdatedOn ?? prescription.CreatedOn,
                    Notes = prescription.MedicationName
                })
                .ToList();
        }

        private static PrescriptionDto MapToDto(Prescription prescription)
        {
            return new PrescriptionDto
            {
                Id = prescription.Id,
                MedicationName = prescription.MedicationName,
                DosageInstructions = prescription.DosageInstructions,
                Status = prescription.Status,
                LastRefillOn = prescription.LastRefillOn,
                NextRefillAvailableOn = prescription.NextRefillAvailableOn,
                AllowAutoRefill = prescription.AllowAutoRefill
            };
        }
    }
}
