using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;

        public EducationController() : this((IEducationService)DependencyResolver.Current.GetService(typeof(IEducationService)))
        {
        }

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public async Task<ActionResult> Resources(string category, string language, bool accessibility = false)
        {
            var query = new ResourceQuery
            {
                Category = category,
                Language = language,
                AccessibilityModeEnabled = accessibility
            };

            var resources = await _educationService.GetResourcesAsync(query);
            return Json(resources, JsonRequestBehavior.AllowGet);
        }
    }
}
