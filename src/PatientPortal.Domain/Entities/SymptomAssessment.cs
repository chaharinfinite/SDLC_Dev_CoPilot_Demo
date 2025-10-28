using System.Collections.Generic;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Entities
{
    public class SymptomAssessment : EntityBase
    {
        private readonly List<string> _suggestedSpecialties = new List<string>();

        private SymptomAssessment()
        {
        }

        public SymptomAssessment(string patientUserId, string symptomSummary)
        {
            PatientUserId = patientUserId;
            SymptomSummary = symptomSummary;
        }

        public string PatientUserId { get; private set; }
        public string SymptomSummary { get; private set; }
        public string TriageLevel { get; private set; }
        public string RecommendedAction { get; private set; }

        public IReadOnlyCollection<string> SuggestedSpecialties => _suggestedSpecialties.AsReadOnly();

        public void SetRecommendation(string triageLevel, string action)
        {
            TriageLevel = triageLevel;
            RecommendedAction = action;
        }

        public void AddSuggestedSpecialty(string specialty)
        {
            _suggestedSpecialties.Add(specialty);
        }
    }
}
