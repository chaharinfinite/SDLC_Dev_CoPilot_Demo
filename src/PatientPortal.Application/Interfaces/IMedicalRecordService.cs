using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<IReadOnlyList<MedicalRecordSummaryDto>> GetRecordsAsync(string patientUserId);
        Task<MedicalRecordDetailDto> GetRecordAsync(Guid recordId);
        Task<IReadOnlyList<HistoricalTrendPoint>> GetHistoricalTrendsAsync(string patientUserId, string metricName);
    }
}
