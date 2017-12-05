using System.Data.Entity.Migrations;
using System.Web.UI.WebControls.WebParts;
using PagedList;
using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;

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

        private PuranoPustakEntities db = new PuranoPustakEntities();
        public ActionResult RegisterList(string searchBy, string search, int? page)
        {
            List<User> list = db.Users.ToList();
                using (PuranoPustakEntities db = new PuranoPustakEntities())
                {
                    var models = new User();
                    models.UserProfiles = db.Users.ToList();
                if (searchBy == "UserName")
                {
                    models.UserProfiles = models.UserProfiles.Where(x => x.UserName.ToLower().StartsWith(search.ToLower())).ToList();
                    return View(models);
                }
                else if (searchBy == "Email")
                {
                    models.UserProfiles = models.UserProfiles.Where(x => x.Email.ToLower().StartsWith(search.ToLower())).ToList();
                    return View(models);
                }
                //else
                //{
                //    return View(models.UserProfiles.Where(x => x.UserName.StartsWith(search)).ToList());
                //}
                return View(models);
                }

                

        }

        public ActionResult Delete(int id)
        {
            using(PuranoPustakEntities db = new PuranoPustakEntities())
            {
                var user = db.Users.Where(x => x.UserID == id).First();
                db.Users.Remove(user);
                db.SaveChanges();

            }
            return RedirectToAction("RegisterList");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (PuranoPustakEntities db = new PuranoPustakEntities())
            {
                var user = db.Users.Find(id);
                return View(user);
            }
            
        }

        [HttpPost]

        public ActionResult Edit(User model)
        {
            try
            {
                using ( PuranoPustakEntities db = new PuranoPustakEntities())
                {
                    var user = db.Users.Where(x => x.UserID == model.UserID).FirstOrDefault();
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.ConfirmPassword = user.Password;
                    db.Users.AddOrUpdate(user);
                    db.SaveChanges();

                }
                return RedirectToAction("RegisterList");
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }

        
    }
}