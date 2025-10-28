using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class SymptomTriageController : Controller
    {
        private readonly ISymptomTriageService _triageService;

        public SymptomTriageController() : this((ISymptomTriageService)DependencyResolver.Current.GetService(typeof(ISymptomTriageService)))
        {
        }

        public SymptomTriageController(ISymptomTriageService triageService)
        {
            _triageService = triageService;
        }

        [HttpPost]
        public async Task<ActionResult> Assess(SymptomAssessmentRequest request)
        {
            if (request == null)
            {
                return Json(new { status = "failed", reason = "Invalid assessment" });
            }

            var assessment = await _triageService.AssessAsync(request);
            return Json(assessment);
        }
    }
}
