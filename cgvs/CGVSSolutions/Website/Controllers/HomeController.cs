using Services;
using Services.ServiceComponents.ImageTypedBehaviour;
using System;
using System.Web.Mvc;

namespace Website.Controllers
{

    [HandleError]
    public class HomeController : Controller
    {
        public HomeController()
        {
            WebConfigurationManager webConfiguration = WebConfigurationManager.Instance;
        }
        public ActionResult Index()
        {
            ViewBag.WebsiteName = "Chhattisgarh Vigyan sabha";
            ViewBag.Title = "Chhattisgarh Vigyan sabha";
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

        public ActionResult commingSoon()
        {
            return PartialView("comingSoon");
        }
    }
}