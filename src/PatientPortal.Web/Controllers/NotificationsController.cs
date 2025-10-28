using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController() : this((INotificationService)DependencyResolver.Current.GetService(typeof(INotificationService)))
        {
        }

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<ActionResult> Schedule(NotificationRequest request)
        {
            if (request == null)
            {
                return Json(new { status = "failed", reason = "Invalid notification" });
            }

            await _notificationService.ScheduleReminderAsync(request);
            return Json(new { status = "scheduled" });
        }

        [HttpGet]
        public async Task<ActionResult> Pending(string userId)
        {
            var pending = await _notificationService.GetPendingNotificationsAsync(userId);
            return Json(pending, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Delivered(Guid id)
        {
            await _notificationService.MarkDeliveredAsync(id);
            return Json(new { status = "delivered" });
        }
    }
}
