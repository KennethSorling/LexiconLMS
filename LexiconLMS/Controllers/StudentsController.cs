using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> _userManager;
        private RoleStore<IdentityRole> _roleStore;
        private RoleManager<IdentityRole> _roleManager;

        public RoleStore<IdentityRole> RoleStore
        {
            get { return _roleStore; }
        }
        public RoleManager<IdentityRole> RoleManager => _roleManager;
        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public StudentsController()
        {
            _roleStore = new RoleStore<IdentityRole>(db);
            _roleManager = new RoleManager<IdentityRole>(_roleStore);
        }

        // GET: Students
        public ActionResult Index()
        {
            var studentRole = RoleManager.FindByName("Student");
            var students = db.Users
                .Where(u => u.Roles.FirstOrDefault().RoleId == studentRole.Id)
                .ToList()
                .ConvertAll(u => new StudentListVM
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    CourseId = u.CourseId.GetValueOrDefault(),
                    CourseName = db.Courses.Find(u.CourseId).Name,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                });
            return View(students);
        }

        //// GET: Students/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Users.Find(id) as Student;
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var vm = new EditStudentAccountVM
        //    {
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        Email = student.Email,
        //        Id = student.Id,
        //        CourseId = student.CourseId.GetValueOrDefault(),
        //        Courses = db.Courses.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList()
        //    };
        //    return View(vm);
        //}

        // GET: Students/Create
        [HttpGet]
        [Authorize(Roles ="Teacher")]
        public ActionResult Create(int? courseId)
        {
            var vm = new CreateStudentAccountVM
            {
                Courses = db.Courses.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };
            if (courseId != null)
            {
                vm.CourseId = courseId.GetValueOrDefault();
            }
            else
            {
                /* This was invoked from the Students list, so we should return there.*/
                vm.ReturnToIndex = true;
            }
            return View(vm);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(CreateStudentAccountVM studentInfo)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = studentInfo.FirstName,
                    LastName = studentInfo.LastName,
                    Email = studentInfo.Email,
                    UserName = studentInfo.Email,
                    CourseId = studentInfo.CourseId,
                    PhoneNumber = studentInfo.PhoneNumber
                };
                
                // Student inherits from ApplicationUser,  so the UserManager should accept it.
                var result = UserManager.Create(student as ApplicationUser, studentInfo.Password);
                if (result.Succeeded)
                {
                    result = UserManager.AddToRole(student.Id, "Student");
                }
                db.SaveChanges();
                TempData["Message"] = "Student Account created.";
                if (studentInfo.ReturnToIndex == true)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Manage", "Courses", new { id = student.CourseId });
            }
            studentInfo.Courses = db.Courses.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            return View(studentInfo);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(string id, bool? returnToIndex = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            var vm = new EditStudentAccountVM {
                ReturnToIndex = returnToIndex.GetValueOrDefault(),
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                CourseId = student.CourseId.GetValueOrDefault(),
                Courses = db.Courses.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList()
            };

            return View(vm);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(EditStudentAccountVM vm)
        {
            if (ModelState.IsValid)
            {
                //var student = UserManager.FindById(vm.Id);
                var student = db.Users.Where(x => x.Id == vm.Id).FirstOrDefault();
                student.FirstName = vm.FirstName;
                student.LastName = vm.LastName;
                student.Email = vm.Email;
                student.CourseId = vm.CourseId;
                student.PhoneNumber = vm.PhoneNumber;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Account Updated";
                if (vm.ReturnToIndex == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Manage", "Courses", new { id = vm.CourseId });
                }
            }
            vm.Courses = db.Courses.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            return View(vm);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            var vm = new EditStudentAccountVM
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CourseId = student.CourseId.GetValueOrDefault(),
                Courses = db.Courses.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList()
            };
            return View(vm);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser student = db.Users.Find(id);
            db.Users.Remove(student);
            db.SaveChanges();
            TempData["Message"] = "Account deleted.";
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
