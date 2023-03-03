using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNTQ.Areas.Admin.Models;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        /*[HttpGet]
        public ActionResult CreateUser() 
        {
            return View();
        }*/
      
    }
}