using System.Threading.Tasks;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;
using PatientPortal.Domain.Entities;

namespace PatientPortal.Application.Services
{
    public class SymptomTriageService : ISymptomTriageService
    {
        private readonly IRepository<SymptomAssessment> _assessmentRepository;
        private readonly IAiTriageClient _aiClient;
        private readonly IUnitOfWork _unitOfWork;

        public SymptomTriageService(
            IRepository<SymptomAssessment> assessmentRepository,
            IAiTriageClient aiClient,
            IUnitOfWork unitOfWork)
        {
            _assessmentRepository = assessmentRepository;
            _aiClient = aiClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<SymptomAssessmentDto> AssessAsync(SymptomAssessmentRequest request)
        {
            var (triageLevel, recommendedAction, specialties) = await _aiClient.AssessAsync(request.SymptomSummary, request.Symptoms, request.IsUrgent);

            var assessment = new SymptomAssessment(request.PatientUserId, request.SymptomSummary);
            assessment.SetRecommendation(triageLevel, recommendedAction);
            if (specialties != null)
            {
                foreach (var specialty in specialties)
                {
                    assessment.AddSuggestedSpecialty(specialty);
                }
            }

            await _assessmentRepository.AddAsync(assessment);
            await _unitOfWork.SaveChangesAsync();

            return new SymptomAssessmentDto
            {
                AssessmentId = assessment.Id.ToString(),
                PatientUserId = assessment.PatientUserId,
                SymptomSummary = assessment.SymptomSummary,
                TriageLevel = assessment.TriageLevel,
                RecommendedAction = assessment.RecommendedAction,
                SuggestedSpecialties = assessment.SuggestedSpecialties
            };
        }
    }
}
