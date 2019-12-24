using Services;
using Services.ServiceComponents.ImageTypedBehaviour;
using System;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private ImageService _websiteImageService = null;
        public HomeController()
        {
            WebConfigurationManager webConfiguration = new WebConfigurationManager();

            _websiteImageService = new ImageService(new WebSiteImageGetBehaviour(webConfiguration.ConnectionString));
        }
        public ActionResult Index()
        {
            ViewBag.WebsiteName = "Chhattisgarh Vigyan sabha";
            //TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            //var date = new DateTime(2016, 11, 23);
            //ViewBag.timeinfo = TimeZoneInfo.ConvertTimeToUtc(date,timeZoneInfo);

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