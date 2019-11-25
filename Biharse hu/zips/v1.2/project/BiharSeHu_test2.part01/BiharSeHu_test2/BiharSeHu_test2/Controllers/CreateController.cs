using BiharSeHu_test2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiharSeHu_test2.Controllers
{
    public class CreateController : Controller
    {
        UserClass user = new UserClass();
        [HttpGet]
        public ActionResult CreateLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLogin(LoginInfo lginfo)
        {
            this.user.loginInfo = lginfo;
            return View("GetCreateAdmin");
        }
        [HttpPost]
        public ActionResult CreateAdmin(UserInfo uinfo)
        {
            this.user.userInfo = uinfo;
            DatabaseContext dbcs = new DatabaseContext();
            dbcs.UserInfoes.Add(user.userInfo);

            dbcs.SaveChanges();
            return View();
        }
    }
}