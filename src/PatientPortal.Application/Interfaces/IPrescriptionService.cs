using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IPrescriptionService
    {
        Task<IReadOnlyList<PrescriptionDto>> GetActivePrescriptionsAsync(string patientUserId);
        Task RequestRefillAsync(Guid prescriptionId);
        Task<IReadOnlyList<PrescriptionHistoryDto>> GetHistoryAsync(string patientUserId);
    }
}
