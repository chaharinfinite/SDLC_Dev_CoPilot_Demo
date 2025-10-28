using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController() : this((IMedicalRecordService)DependencyResolver.Current.GetService(typeof(IMedicalRecordService)))
        {
        }

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        public async Task<ActionResult> List(string userId)
        {
            var records = await _medicalRecordService.GetRecordsAsync(userId);
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var record = await _medicalRecordService.GetRecordAsync(id);
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Trends(string userId, string metric)
        {
            var trends = await _medicalRecordService.GetHistoricalTrendsAsync(userId, metric);
            return Json(trends, JsonRequestBehavior.AllowGet);
        }
    }
}
