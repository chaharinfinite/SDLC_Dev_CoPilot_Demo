using System;
using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface ITelehealthService
    {
        Task<TelehealthSessionDto> CreateSessionAsync(TelehealthSessionRequest request);
        Task StartSessionAsync(Guid sessionId);
        Task CompleteSessionAsync(Guid sessionId);
        Task FlagTechnicalIssueAsync(Guid sessionId, string description);
    }
}
