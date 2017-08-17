using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Upload(int? courseId, int? moduleId, int? activityId, string returnTo)
        {
            var vm = new UploadVM
            {
                 CourseId = courseId,
                 ModuleId = moduleId,
                 ActivityId = activityId,
                 ReturnTo = returnTo
            };

            return View(vm);
        }



        // GET: Documents
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.MimeType).Include(d => d.Status);
            return View(documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            ViewBag.MimeTypeId = new SelectList(db.MimeTypes, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MimeTypeId,StatusId,Filename,FileSize,Title,FileType,ModuleId,CourseId,ActivityId,ApplicationUserId,DateUploaded,DeadLine")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MimeTypeId = new SelectList(db.MimeTypes, "Id", "Name", document.MimeTypeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", document.StatusId);
            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.MimeTypeId = new SelectList(db.MimeTypes, "Id", "Name", document.MimeTypeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", document.StatusId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MimeTypeId,StatusId,Filename,FileSize,Title,FileType,ModuleId,CourseId,ActivityId,ApplicationUserId,DateUploaded,DeadLine")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MimeTypeId = new SelectList(db.MimeTypes, "Id", "Name", document.MimeTypeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", document.StatusId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
