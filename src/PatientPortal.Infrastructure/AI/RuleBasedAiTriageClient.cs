using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Infrastructure.AI
{
    public class RuleBasedAiTriageClient : IAiTriageClient
    {
        public Task<(string triageLevel, string recommendedAction, IReadOnlyCollection<string> specialties)> AssessAsync(string symptomSummary, IReadOnlyCollection<string> symptoms, bool isUrgent)
        {
            var lowerSummary = symptomSummary?.ToLowerInvariant() ?? string.Empty;
            var triageLevel = "Routine";
            var recommendedAction = "Schedule a visit when convenient.";
            var specialties = new List<string>();

            if (isUrgent || ContainsAny(symptoms, "chest pain", "shortness of breath"))
            {
                triageLevel = "Emergency";
                recommendedAction = "Call emergency services immediately.";
            }
            else if (ContainsAny(symptoms, "fever", "cough") || lowerSummary.Contains("fever"))
            {
                triageLevel = "Priority";
                recommendedAction = "Book a same-day telehealth visit.";
                specialties.Add("Internal Medicine");
            }
            else if (ContainsAny(symptoms, "rash", "skin"))
            {
                triageLevel = "Routine";
                recommendedAction = "Consult dermatology within a week.";
                specialties.Add("Dermatology");
            }

            return Task.FromResult((triageLevel, recommendedAction, (IReadOnlyCollection<string>)specialties));
        }

        private static bool ContainsAny(IReadOnlyCollection<string> symptoms, params string[] keywords)
        {
            if (symptoms == null || symptoms.Count == 0)
            {
                return false;
            }

            var normalized = symptoms.Select(symptom => symptom?.ToLowerInvariant() ?? string.Empty).ToList();
            foreach (var keyword in keywords)
            {
                if (normalized.Any(value => value.Contains(keyword)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
