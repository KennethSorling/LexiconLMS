using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Upload(int? courseId, int? moduleId, int? activityId, int? purposeId, DateTime? deadLine, string returnTo)
        {
            var purposes = db.Purposes.ToList().ConvertAll(d => new SelectListItem
            {
                Text = d.Name,
                Value = $"{d.Id}",
                Selected = purposeId != null ? purposeId == d.Id ? true : false : false,
            });

            var vm = new UploadVM
            {
                CourseId = courseId,
                ModuleId = moduleId,
                ActivityId = activityId,
                PurposeId = purposeId == null ? 1 : (int)purposeId,
                Purposes = purposes,
                ReturnTo = returnTo
            };

            ViewBag.PurposeId = purposes;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Upload([Bind(Include ="CourseId,ModuleId,ActivityId,PurposeId,DeadLine,ReturnTo")]UploadVM vm, HttpPostedFileBase uploadFile)
        {
            // Verify that the user selected a file
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                //Trim off possible path info from IE clients
                string localFile = Path.GetFileName(uploadFile.FileName);
                
                //obtain a local path to our uploads folder
                var path = Server.MapPath("~/uploads");
                string fileName;

                //ensure it exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(Server.MapPath("~/uploads"), localFile);

                /* Avoid collision with already uploaded files*/
                int fileIndex = 0;
                string extension = path.Substring(path.LastIndexOf(".") +1);
                string basePath = path.Substring(0, path.LastIndexOf(".") - 1);
                while (System.IO.File.Exists(path))
                {
                    path = $"{basePath}({++fileIndex}).{extension}";
                }
                //  And, try to save. Watch for problems.
                try
                {
                    uploadFile.SaveAs(path);
                }
                catch (IOException  e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                if (ModelState.IsValid)
                {
                    //if we get here, we probably managed to save the thing.
                    //now, to create a record for it.

                    //shave off the path. Since we may have renamed the file, we cannot rely
                    //on the submitted filename.
                    fileName = path.Substring(path.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);


                    var me = db.Users.Find(User.Identity.GetUserId());

                    // try to find the mime type for this file.
                    var mimeType = db.MimeTypes.Where(mt => mt.Name == uploadFile.ContentType).FirstOrDefault();
                    if (mimeType == null)
                    {
                        //this gives us "applciation/octet-stream", which is a good default compromise
                        mimeType = db.MimeTypes.Where(mt => mt.DefaultExtension == "").FirstOrDefault();
                    }

                    var doc = new Document{
                        Filename = fileName,
                        FileSize = uploadFile.ContentLength,
                        FileType = uploadFile.ContentType,
                        ApplicationUserId = me.Id,
                        DateUploaded = DateTime.Now,
                        PurposeId = vm.PurposeId,
                        ActivityId = vm.ActivityId,
                        ModuleId = vm.ModuleId,
                        CourseId = vm.CourseId,
                        DeadLine = vm.DeadLine,
                        Owner = me,
                        MimeType = mimeType,
                        MimeTypeId = mimeType.Id,
                        Purpose = db.Purposes.Find(vm.PurposeId),
                    };

                    if (User.IsInRole("Teacher"))
                    {
                        //Teacher uploads get a status of Issued by default
                        doc.Status = db.Statuses.FirstOrDefault();
                        doc.StatusId = doc.Status.Id;
                    }
                    else if (User.IsInRole("Student"))
                    {
                        // Hand-In?
                        if (vm.PurposeId == 7)
                        {
                            //submitted
                            doc.Status = db.Statuses.Find(3);
                        }
                        else
                        {
                            doc.Status = db.Statuses.FirstOrDefault();
                        }
                    }
                    db.Documents.Add(doc);
                    db.SaveChanges();

                    TempData["Message"] = $"File {fileName} received!";

                    if (vm.ReturnTo != null)
                    {
                        return Redirect(vm.ReturnTo);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "You forgot to select a file.");
            }
            var purposes = db.Purposes.ToList().ConvertAll(d => new SelectListItem
            {
                Text = d.Name,
                Value = $"{d.Id}",
                Selected = (vm.PurposeId  == d.Id)
            });

            ViewBag.PurposeId = purposes;
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
