using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNTQ.Areas.Admin.Models;
using WebNTQ.Common;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Admin/Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Register(model.Email, model.UserName);
                if (result == 1)
                {
                    return RedirectToAction("Index", "Login");
                }
                else if (result == 0)
                {
 
                    ModelState.AddModelError("Email", "UserName đã tồn tại!");
                }
                else
                {
                    
                    ModelState.AddModelError("Email", "Email đã tồn tại!");
                }
            }
            return View("Index");
        }
    }
}