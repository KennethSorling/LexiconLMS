using System.Web.Mvc;
using System.Web.Routing;

namespace LexiconLMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Dashboard",
            url: "dashboard",
            defaults: new { controller = "DashboardVMs", action = "ShowDashboard" }
            );

            routes.MapRoute(
            name: "Schedule",
            url: "schedule/{courseId}",
            defaults: new { controller = "ScheduleVMs", action = "ShowSchedule", courseId = UrlParameter.Optional },
            constraints: new { courseId = @"\d+" }
            );

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}