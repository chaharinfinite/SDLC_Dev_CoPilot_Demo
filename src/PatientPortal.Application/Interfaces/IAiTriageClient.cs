using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientPortal.Application.Interfaces
{
    public interface IAiTriageClient
    {
        Task<(string triageLevel, string recommendedAction, IReadOnlyCollection<string> specialties)> AssessAsync(string symptomSummary, IReadOnlyCollection<string> symptoms, bool isUrgent);
    }
}
