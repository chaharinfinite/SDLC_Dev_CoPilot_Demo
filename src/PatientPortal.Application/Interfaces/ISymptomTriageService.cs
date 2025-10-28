using System.Threading.Tasks;
using PatientPortal.Application.DTOs;

namespace PatientPortal.Application.Interfaces
{
    public interface ISymptomTriageService
    {
        Task<SymptomAssessmentDto> AssessAsync(SymptomAssessmentRequest request);
    }
}
