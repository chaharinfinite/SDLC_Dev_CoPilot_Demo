using System.Web.Mvc;

namespace PatientPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Json(new { application = "PatientPortal", status = "Healthy" }, JsonRequestBehavior.AllowGet);
        }
    }
}
