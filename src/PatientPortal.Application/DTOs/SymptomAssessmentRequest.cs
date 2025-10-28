using System.Collections.Generic;

namespace PatientPortal.Application.DTOs
{
    public class SymptomAssessmentRequest
    {
        public string PatientUserId { get; set; }
        public string SymptomSummary { get; set; }
        public IReadOnlyCollection<string> Symptoms { get; set; }
        public bool IsUrgent { get; set; }
    }
}
