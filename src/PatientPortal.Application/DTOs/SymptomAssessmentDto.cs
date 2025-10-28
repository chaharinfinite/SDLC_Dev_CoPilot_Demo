using System.Collections.Generic;

namespace PatientPortal.Application.DTOs
{
    public class SymptomAssessmentDto
    {
        public string AssessmentId { get; set; }
        public string PatientUserId { get; set; }
        public string SymptomSummary { get; set; }
        public string TriageLevel { get; set; }
        public string RecommendedAction { get; set; }
        public IReadOnlyCollection<string> SuggestedSpecialties { get; set; }
    }
}
