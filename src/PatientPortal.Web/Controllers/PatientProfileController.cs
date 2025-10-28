using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class PatientProfileController : Controller
    {
        private readonly IPatientProfileService _profileService;

        public PatientProfileController() : this((IPatientProfileService)DependencyResolver.Current.GetService(typeof(IPatientProfileService)))
        {
        }

        public PatientProfileController(IPatientProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string userId)
        {
            var profile = await _profileService.GetProfileAsync(userId);
            return Json(profile, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Update(PatientProfileUpdateRequest request)
        {
            await _profileService.UpdateProfileAsync(request);
            return Json(new { status = "updated" });
        }

        [HttpPost]
        public async Task<ActionResult> Accessibility(string userId, bool enabled)
        {
            await _profileService.EnableAccessibilityAsync(userId, enabled);
            return Json(new { status = "updated" });
        }

        [HttpPost]
        public async Task<ActionResult> Preferences(CommunicationPreferenceDto preference)
        {
            if (preference == null || string.IsNullOrWhiteSpace(preference.PatientUserId))
            {
                return Json(new { status = "failed", reason = "Invalid preference payload" });
            }

            await _profileService.SetCommunicationPreferenceAsync(preference.PatientUserId, preference);
            return Json(new { status = "updated" });
        }

        [HttpPost]
        public async Task<ActionResult> RegisterDevice(DeviceIntegrationDto device)
        {
            if (device == null || string.IsNullOrWhiteSpace(device.PatientUserId))
            {
                return Json(new { status = "failed", reason = "Invalid device payload" });
            }

            await _profileService.RegisterDeviceAsync(device.PatientUserId, device);
            return Json(new { status = "registered" });
        }
    }
}
