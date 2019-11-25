using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiharSeHu_test2.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BiharSeHu_test2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
    }
}