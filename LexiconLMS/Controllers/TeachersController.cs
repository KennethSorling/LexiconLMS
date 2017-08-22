using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeachersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> _userManager;
        private RoleStore<IdentityRole> _roleStore;
        private RoleManager<IdentityRole> _roleManager;

        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public RoleStore<IdentityRole> RoleStore
        {
            get { return _roleStore; }
        }
        public RoleManager<IdentityRole> RoleManager => _roleManager;

        public TeachersController()
        {
            _roleStore = new RoleStore<IdentityRole>(db);
            _roleManager = new RoleManager<IdentityRole>(_roleStore);
        }

        // GET: Teachers
        [Authorize(Roles = "Teacher")]
        public ActionResult Index(string sortOn, string searchOn = "")
        {
            var teacherRole = RoleManager.FindByName("Teacher");
            var users = db.Users
                .Where(u => u.Roles.FirstOrDefault().RoleId == teacherRole.Id)
                .ToList();

            var teachers = users.ToList().ConvertAll(u => new TeacherListVM
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Id = u.Id,
                PhoneNumber = u.PhoneNumber
            }
            );

            if (sortOn != null && teachers != null)
            {
                switch (sortOn)
                {
                    case "firstname_asc":
                        teachers = teachers.OrderByDescending(s => s.FirstName).ToList();
                        ViewBag.SortOrderFirstName = "firstname_desc";
                        break;
                    case "firstname_desc":
                        teachers = teachers.OrderBy(s => s.FirstName).ToList();
                        ViewBag.SortOrderFirstName = "firstname_asc";
                        break;
                    case "lastname_asc":
                        teachers = teachers.OrderByDescending(s => s.LastName).ToList();
                        ViewBag.SortOrderLastName = "lastname_desc";
                        break;
                    case "lastname_desc":
                        teachers = teachers.OrderBy(s => s.LastName).ToList();
                        ViewBag.SortOrderLastName = "lastname_asc";
                        break;
                    case "email_asc":
                        teachers = teachers.OrderByDescending(s => s.Email).ToList();
                        ViewBag.SortOrderEmail = "email_desc";
                        break;
                    case "email_desc":
                        teachers = teachers.OrderBy(s => s.Email).ToList();
                        ViewBag.SortOrderEmail = "email_asc";
                        break;
                    case "phonenumber_asc":
                        teachers = teachers.OrderByDescending(s => s.PhoneNumber).ToList();
                        ViewBag.SortOrderPhoneNumber = "phonenumber_desc";
                        break;
                    case "phonenumber_desc":
                        teachers = teachers.OrderBy(s => s.PhoneNumber).ToList();
                        ViewBag.SortOrderPhoneNumber = "phonenumber_asc";
                        break;
                    default:
                        ViewBag.SortOrderFirstName = "firstname_desc";
                        ViewBag.SortOrderLastName = "lastname_desc";
                        ViewBag.SortOrderEmail = "email_desc";
                        ViewBag.SortOrderPhoneNumber = "phonenumber_desc";
                        break;
                }
            }
            else
            {
                ViewBag.SortOrderFirstName = "firstname_desc";
                ViewBag.SortOrderLastName = "lastname_desc";
                ViewBag.SortOrderEmail = "email_desc";
                ViewBag.SortOrderPhoneNumber = "phonenumber_desc";
            }

            if (!String.IsNullOrEmpty(searchOn))
            {
                teachers = teachers.Where(s => s.FirstName.ToLower().Contains(searchOn.ToLower()))
                                                    .Concat(teachers.Where(s => s.LastName.ToLower().Contains(searchOn.ToLower())))
                                                    .Concat(teachers.Where(s => s.Email.ToLower().Contains(searchOn.ToLower())))
                                                    .Distinct()
                                                    .ToList();
                ViewBag.SearchOn = searchOn;
            }

            return View(teachers);
        }

        //// GET: Teachers/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Teacher teacher = db.Users.Find(id) as Teacher;
        //    if (teacher == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var vm = new EditTeacherAccountVM {
        //        Id = teacher.Id,
        //        FirstName =teacher.FirstName,
        //        LastName =teacher.LastName,
        //        Email = teacher.Email
        //    };

        //    return View(vm);
        //}


        //// POST: Teachers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,LastName,CourseId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] Teacher teacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(teacher);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return View(teacher);
        //}

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            var vm = new CreateTeacherAccountVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(CreateTeacherAccountVM details)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = details.Email,
                    FirstName = details.FirstName,
                    LastName = details.LastName,
                    Email = details.Email,
                    LastLogon = new DateTime(1970,1,1)
                    
                };
                var result = UserManager.Create(user, details.Password);
                if (!result.Succeeded)
                {
                    throw new System.Exception(string.Join("\n", result.Errors));
                }
                UserManager.AddToRole(user.Id, "Teacher");
                details.Id = user.Id;
                TempData["Message"] = "Teacher Account created.";
                return RedirectToAction("Index");
            }
            return View(details);
        }
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            if (user == null)
            {
                throw new System.Exception("User not found.");
            }
            /*
             We don't yet know whether the account to be edited belongs to a teacher or a student,
             So we create a VM for the StudentAccountVM, which is inherently both
             */
            var vm = new EditTeacherAccountVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id
            };
            //if (UserManager.IsInRole(user.Id, "Student"))
            //{
            //    vm.Courses = db.Courses.ToList();
            //    vm.CourseId = user.CourseId.GetValueOrDefault();
            //}
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTeacherAccountVM accountInfo)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(accountInfo.Id);
                if (user != null)
                {
                    user.Email = accountInfo.Email;
                    user.FirstName = accountInfo.FirstName;
                    user.LastName = accountInfo.LastName;
                    user.PhoneNumber = accountInfo.PhoneNumber;
                }
                UserManager.Update(user);
                //return RedirectToAction("Details", new { id = accountInfo.Id });
                return RedirectToAction("Index");
            }
            return View(accountInfo);
        }


        //// POST: Teachers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,CourseId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] Teacher teacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(teacher).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(teacher);
        //}

        // GET: Teachers/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Teacher teacher = db.Users.Find(id) as Teacher;
            ApplicationUser teacher = db.Users.Where(t => t.Id == id).FirstOrDefault();
            if (teacher == null)
            {
                return HttpNotFound();
            }
            var vm = new EditTeacherAccountVM
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber
            };
            return View(vm);
        }

        // POST: Teachers/Delete/5
        [Authorize(Roles = "Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser teacher = db.Users.Find(id);
            db.Users.Remove(teacher);
            db.SaveChanges();
            TempData["Message"] = "Account Deleted.";
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
