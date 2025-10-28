using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IRepository<MedicalRecord> _medicalRecordRepository;
        private readonly IReadRepository<LabResult> _labResultRepository;

        public MedicalRecordService(IRepository<MedicalRecord> medicalRecordRepository, IReadRepository<LabResult> labResultRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _labResultRepository = labResultRepository;
        }

        public async Task<IReadOnlyList<MedicalRecordSummaryDto>> GetRecordsAsync(string patientUserId)
        {
            var records = await _medicalRecordRepository.SearchAsync(record => record.PatientUserId == patientUserId);
            return records
                .OrderByDescending(record => record.AvailableOn)
                .Select(record => new MedicalRecordSummaryDto
                {
                    Id = record.Id,
                    RecordType = record.RecordType,
                    Summary = record.Summary,
                    AvailableOn = record.AvailableOn,
                    IsDownloadable = record.IsDownloadable
                })
                .ToList();
        }

        public async Task<MedicalRecordDetailDto> GetRecordAsync(Guid recordId)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(recordId);
            if (record == null)
            {
                return null;
            }

            return new MedicalRecordDetailDto
            {
                Id = record.Id,
                RecordType = record.RecordType,
                Summary = record.Summary,
                DocumentUri = record.DocumentUri,
                AvailableOn = record.AvailableOn,
                Trends = Array.Empty<HistoricalTrendPoint>()
            };
        }

        public async Task<IReadOnlyList<HistoricalTrendPoint>> GetHistoricalTrendsAsync(string patientUserId, string metricName)
        {
            var labResults = await _labResultRepository.SearchAsync(result => result.PatientUserId == patientUserId);
            var metrics = labResults
                .OrderBy(result => result.ReleasedOn)
                .SelectMany(result => result.Metrics.Select(metric => new { Result = result, Metric = metric }))
                .Where(pair => string.Equals(pair.Metric.Name, metricName, StringComparison.OrdinalIgnoreCase))
                .Select(pair => new HistoricalTrendPoint
                {
                    Timestamp = pair.Result.ReleasedOn,
                    MetricName = pair.Metric.Name,
                    Value = pair.Metric.Value,
                    Unit = pair.Metric.Unit
                })
                .ToList();

            return metrics;
        }
    }
}
