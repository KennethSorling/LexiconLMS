using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Index", "Courses");
            }
            else if (User.IsInRole("Student"))
            {
                //Direct the student to te dashboard
                return RedirectToAction("ShowDashboard", "DashboardVMs");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}