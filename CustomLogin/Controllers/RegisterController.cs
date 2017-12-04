using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomLogin.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        // GET: Register
        public ActionResult Register(int id = 0)
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]

        public ActionResult Register(User model)
        {
            using (PuranoPustakEntities db = new PuranoPustakEntities())
            {
                if(db.Users.Any(x => x.UserName == model.UserName))
                {
                    ViewBag.DuplicateMessage = "User Name already exist!";
                    return View("Register", model);
                }
                else if(db.Users.Any(x => x.Email == model.Email))
                {
                    ViewBag.DuplicateMessage = "Email already exist!";
                    return View("Register", model);
                }
                db.Users.Add(model);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successfull";
                return View("Register", new User());

        }

        public ActionResult RegisterList()
        {
            using (PuranoPustakEntities db = new PuranoPustakEntities())
            {
                var models = new User();
                models. UserProfiles = db.Users.ToList();
                return View(models);
            }
            
        }
    }
}