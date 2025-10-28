using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PatientPortal.Application.DTOs;
using PatientPortal.Application.Interfaces;

namespace PatientPortal.Web.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentsController() : this((IDocumentService)DependencyResolver.Current.GetService(typeof(IDocumentService)))
        {
        }

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult> List(string userId)
        {
            var documents = await _documentService.GetDocumentsAsync(userId);
            return Json(documents, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(string userId, string documentType, HttpPostedFileBase file)
        {
            if (file == null)
            {
                return Json(new { status = "failed", reason = "File missing" });
            }

            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                var request = new DocumentUploadRequest
                {
                    PatientUserId = userId,
                    DocumentType = documentType,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Content = memoryStream.ToArray()
                };

                await _documentService.UploadDocumentAsync(request);
            }

            return Json(new { status = "uploaded" });
        }

        [HttpPost]
        public async Task<ActionResult> Verify(System.Guid id)
        {
            await _documentService.MarkVerifiedAsync(id);
            return Json(new { status = "verified" });
        }
    }
}
