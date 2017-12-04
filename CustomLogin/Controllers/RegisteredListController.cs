using CustomLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomLogin.Controllers
{
    public class RegisteredListController : Controller
    {
        // GET: RegisteredList
        public ActionResult RegisteredList()
        {
            PuranoPustakEntities db = new PuranoPustakEntities();
            var users = db.Users.ToList();
            return View();
        }
    }
}