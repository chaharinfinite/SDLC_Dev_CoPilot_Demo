using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class MessagingController : Controller
    {
        private readonly IMessagingService _messagingService;

        public MessagingController() : this((IMessagingService)DependencyResolver.Current.GetService(typeof(IMessagingService)))
        {
        }

        public MessagingController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [HttpGet]
        public async Task<ActionResult> Inbox(string userId)
        {
            var messages = await _messagingService.GetInboxAsync(userId);
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Send(MessageComposeRequest request)
        {
            if (request == null)
            {
                return Json(new { status = "failed", reason = "Invalid request" });
            }

            var message = await _messagingService.SendMessageAsync(request);
            return Json(message);
        }

        [HttpPost]
        public async Task<ActionResult> Read(Guid id)
        {
            await _messagingService.MarkAsReadAsync(id);
            return Json(new { status = "read" });
        }

        [HttpPost]
        public async Task<ActionResult> Archive(Guid id)
        {
            await _messagingService.ArchiveMessageAsync(id);
            return Json(new { status = "archived" });
        }
    }
}
