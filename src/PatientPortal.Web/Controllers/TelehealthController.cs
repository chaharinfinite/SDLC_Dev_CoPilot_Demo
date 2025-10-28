using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class TelehealthController : Controller
    {
        private readonly ITelehealthService _telehealthService;

        public TelehealthController() : this((ITelehealthService)DependencyResolver.Current.GetService(typeof(ITelehealthService)))
        {
        }

        public TelehealthController(ITelehealthService telehealthService)
        {
            _telehealthService = telehealthService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSession(TelehealthSessionRequest request)
        {
            if (request == null)
            {
                return Json(new { status = "failed", reason = "Invalid telehealth request" });
            }

            var session = await _telehealthService.CreateSessionAsync(request);
            return Json(session);
        }

        [HttpPost]
        public async Task<ActionResult> Start(Guid id)
        {
            await _telehealthService.StartSessionAsync(id);
            return Json(new { status = "started" });
        }

        [HttpPost]
        public async Task<ActionResult> Complete(Guid id)
        {
            await _telehealthService.CompleteSessionAsync(id);
            return Json(new { status = "completed" });
        }

        [HttpPost]
        public async Task<ActionResult> FlagIssue(Guid id, string description)
        {
            await _telehealthService.FlagTechnicalIssueAsync(id, description);
            return Json(new { status = "flagged" });
        }
    }
}
