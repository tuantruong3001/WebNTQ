using Model.Dao;
using Model.EF;
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
        public ActionResult Register(RegisterModel registermodel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.RegisterCheck(registermodel.Email, registermodel.UserName);            
                if (result == 1)
                {
                    var user = new User
                    {
                        UserName = registermodel.UserName,
                        Email = registermodel.Email,
                        Password = registermodel.Password,
                        Role = 0,
                        CreateAt = DateTime.Now,
                        Status = true,
                    };
                    dao.Insert(user);
                    return RedirectToAction("Index", "Login");
                }
                else if (result == -1)
                {
 
                    ModelState.AddModelError("", "UserName đã tồn tại!");
                }
                else
                {
                    
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
            }
            return View("Index");
        }
    }
}