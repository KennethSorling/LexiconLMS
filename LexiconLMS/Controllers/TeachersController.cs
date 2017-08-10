using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles ="Teacher")]
    public class TeachersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> _userManager;
        private RoleStore<IdentityRole> _roleStore;
        private RoleManager<IdentityRole>_roleManager;

        public UserManager<ApplicationUser> UserManager
        {
            get {
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
        public ActionResult Index()
        {
            var teacherRole = RoleManager.FindByName("Teacher");
            var users = db.Users.Where(u => u.Roles.FirstOrDefault().RoleId == teacherRole.Id);
            var teachers = users.ToList().ConvertAll(u => new TeacherListVM
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Id = u.Id
                }
            );
            return View(teachers);
        }

        // GET: Teachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Users.Find(id) as Teacher;
            if (teacher == null)
            {
                return HttpNotFound();
            }
            var vm = new EditTeacherAccountVM {
                Id = teacher.Id,
                FirstName =teacher.FirstName,
                LastName =teacher.LastName,
                Email = teacher.Email
            };

            return View(vm);
        }


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
                    Email = details.Email
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
            else
            {
                throw new System.Exception("Modelstate was not valid.");
            }
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
                }
                UserManager.Update(user);
                //return RedirectToAction("Details", new { id = accountInfo.Id });
                return RedirectToAction("Index");
            }
            else
            {
                throw new System.Exception("Modelstate Invalid");
            }
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
                Id=teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
            return View(vm);
        }

        // POST: Teachers/Delete/5
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
