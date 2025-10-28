using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController() : this((IPrescriptionService)DependencyResolver.Current.GetService(typeof(IPrescriptionService)))
        {
        }

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult> Active(string userId)
        {
            var prescriptions = await _prescriptionService.GetActivePrescriptionsAsync(userId);
            return Json(prescriptions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RequestRefill(Guid id)
        {
            await _prescriptionService.RequestRefillAsync(id);
            return Json(new { status = "refill-requested" });
        }

        [HttpGet]
        public async Task<ActionResult> History(string userId)
        {
            var history = await _prescriptionService.GetHistoryAsync(userId);
            return Json(history, JsonRequestBehavior.AllowGet);
        }
    }
}
