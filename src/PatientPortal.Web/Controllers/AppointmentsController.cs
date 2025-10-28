using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController() : this((IAppointmentService)DependencyResolver.Current.GetService(typeof(IAppointmentService)))
        {
        }

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult> Upcoming(string userId)
        {
            var result = await _appointmentService.GetUpcomingAppointmentsAsync(userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Schedule(AppointmentRequest request)
        {
            if (request == null)
            {
                return Json(new { status = "failed", reason = "Invalid request" });
            }

            var appointment = await _appointmentService.ScheduleAppointmentAsync(request);
            return Json(appointment);
        }

        [HttpPost]
        public async Task<ActionResult> Reschedule(Guid id, AppointmentRescheduleRequest request)
        {
            await _appointmentService.RescheduleAppointmentAsync(id, request);
            return Json(new { status = "rescheduled" });
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(Guid id, string cancelledBy)
        {
            await _appointmentService.CancelAppointmentAsync(id, cancelledBy);
            return Json(new { status = "cancelled" });
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(Guid id)
        {
            await _appointmentService.ConfirmAppointmentAsync(id);
            return Json(new { status = "confirmed" });
        }
    }
}
