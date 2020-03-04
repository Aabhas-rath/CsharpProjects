using log4net;
using Services;
using Services.ServiceComponents.ImageTypedBehaviour;
using System;
using System.Web.Mvc;

namespace Website.Controllers
{

    [HandleError]
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));
        private ImageService _websiteImageService = null;
        public HomeController()
        {
            WebConfigurationManager webConfiguration = WebConfigurationManager.Instance;

            _websiteImageService = new ImageService(new WebSiteImageGetBehaviour(webConfiguration.ConnectionString));
        }
        public ActionResult Index()
        {
            ViewBag.WebsiteName = "Chhattisgarh Vigyan sabha";
            //TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            //var date = new DateTime(2016, 11, 23);
            //ViewBag.timeinfo = TimeZoneInfo.ConvertTimeToUtc(date,timeZoneInfo);
            Log.Debug("loading view ");
            ApplicationEventLogger.LogApplication("loading view ");
            return View();
        }

        public ActionResult comingSoon()
        {
            Log.Debug("Coming soon loaded");
            ApplicationEventLogger.LogApplication("Coming soon loaded");
            return PartialView("comingSoon");
        }
    }
}