using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomLogin.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Autherize(CustomLogin.Models.User user)
        {
            using (PuranoPustakEntities db = new PuranoPustakEntities())
            {
                var userLogin = db.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
                if(userLogin == null)
                {
                    user.ErrorMessage = "Entered User Name or Password is incorrect!";
                    return View("Index", user);
                }
                else
                {
                    Session["userID"] = userLogin.UserID;
                    Session["UserName"] = userLogin.UserName;
                    Session["Email"] = userLogin.Email;
                    return RedirectToAction("Index", "Dashboard");
                }
            }
        }

        public ActionResult Logout()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}