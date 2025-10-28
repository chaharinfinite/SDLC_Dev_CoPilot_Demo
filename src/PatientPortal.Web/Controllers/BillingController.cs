using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillingService _billingService;

        public BillingController() : this((IBillingService)DependencyResolver.Current.GetService(typeof(IBillingService)))
        {
        }

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        public async Task<ActionResult> Statements(string userId)
        {
            var statements = await _billingService.GetStatementsAsync(userId);
            return Json(statements, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Pay(PaymentRequest request)
        {
            if (request == null)
            {
                return Json(new { status = "failed", reason = "Invalid payment" });
            }

            var receipt = await _billingService.PayStatementAsync(request);
            return Json(receipt);
        }

        [HttpGet]
        public async Task<ActionResult> Payments(string userId)
        {
            var receipts = await _billingService.GetPaymentHistoryAsync(userId);
            return Json(receipts, JsonRequestBehavior.AllowGet);
        }
    }
}
