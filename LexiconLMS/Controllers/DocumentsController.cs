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
    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ActionResult SetStatus(int documentId, int statusId)
        {
            var doc = db.Documents.Find(documentId);
            if (doc != null)
            {
                var status = db.Statuses.Find(statusId);
                if (status != null)
                {
                    doc.Status = status;
                    doc.StatusId = status.Id;
                    db.Entry(doc).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Document status updated.";
                }
            }
           return RedirectToAction("Review", "Documents", new { id = documentId });
        }
        [Authorize(Roles ="Teacher")]
        public ActionResult Review(int id)
        {
            var doc = db.Documents.Find(id);
            return View(doc);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Approve(int id)
        {
            return SetStatus(id, 5);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Fail(int id)
        {
            return SetStatus(id, 6);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Retry(int id)
        {
            return SetStatus(id, 4);
        }

        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Upload(int? courseId, int? moduleId, int? activityId, int? purposeId, DateTime? deadLine, string returnTo)
        {
            returnTo = returnTo ?? Request.ServerVariables["HTTP_REFERER"];
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
            if (activityId != null)
            {
                var activity = db.Activities.Find(activityId);
                vm.ActivityName = activity.Name;
                moduleId = activity.ModuleId;
            }
            if (moduleId != null)
            {
                var module = db.Modules.Find(moduleId);
                vm.ModuleName = module.Name;
                courseId = module.CourseId;
            }
            if (courseId != null)
            {
                var course = db.Courses.Find(courseId);
                vm.CourseName = course.Name;
            }
            ViewBag.ReturnUrl = Request.ServerVariables["HTTP_REFERER"];
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
                        OwnerId = me.Id,
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
                        /* Student can only upload hand-ins */
                        doc.PurposeId = 7;  //student hand-in
                        doc.StatusId = 3;   //submitted
                        doc.Status = db.Statuses.Find(3);
                        doc.Purpose = db.Purposes.Find(7);

                        /* Find any Exercise description asosciated with this view.*/
                        var exercise = db.Documents.Where(e => (e.PurposeId == 5) && (e.ActivityId == doc.ActivityId)).FirstOrDefault();
                        if (exercise != null)
                        {
                            doc.DeadLine = exercise.DeadLine;
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


        // GET: Documents/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            var returnUrl = Request.ServerVariables["HTTP_REFERER"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Student") && document.OwnerId != User.Identity.GetUserId())
            {
                TempData["Message"] = "You are not authorized to edit any documents but your own.";
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.MimeTypeId = new SelectList(db.MimeTypes, "Id", "Name", document.MimeTypeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", document.StatusId);
            ViewBag.PurposeId = new SelectList(db.Purposes, "Id", "Name", document.PurposeId);

            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MimeTypeId,StatusId,Filename,FileSize,Title,FileType,ModuleId,CourseId,ActivityId,ApplicationUserId,DateUploaded,DeadLine")] Document document, string  returnUrl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(returnUrl);
            }
            ViewBag.PurposeId = new SelectList(db.Purposes, "Id", "Name", document.PurposeId);
            ViewBag.MimeTypeId = new SelectList(db.MimeTypes, "Id", "Name", document.MimeTypeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", document.StatusId);
            ViewBag.ReturnUrl = returnUrl;
            return View(document);
        }

        // GET: Documents/Delete/5
        [Authorize(Roles ="Teacher, Student")]
        [HttpGet]
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

            string returnUrl = Request.ServerVariables["HTTP_REFERER"];
            /* Rule: Students can only delete documents they uploaded.*/
            /* Teachers are allowed to delete any document for any reason. */

            if (User.IsInRole("Student"))
            {
                if (document.OwnerId != User.Identity.GetUserId())
                {
                    TempData["Message"] = "You're only allowed to delete documents you created yourself.";
                    return Redirect(returnUrl);
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return HttpNotFound();
            }
            /*
             * Find the file in the file system, and delete that as well.
             */
            var path = Path.Combine(Server.MapPath("~/uploads"), document.Filename);

            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (IOException ex)
                {
                    TempData["Message"] = ex.Message;
                }
            }
            if (!System.IO.File.Exists(path))
            {
                db.Documents.Remove(document);
                db.SaveChanges();
                TempData["Message"] = $"File '{document.Filename}' deleted.";
            }
            return Redirect(returnUrl);
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
