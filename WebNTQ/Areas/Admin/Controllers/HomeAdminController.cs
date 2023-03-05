using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNTQ.Areas.Admin.Models;
using WebNTQ.Common;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseController
    {
        [HttpGet]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var user = new UserDao().GetByID(id);
            return View(user);
        }
        
        /*[HttpGet]
        public ActionResult Index(int id)
        {
            Session["Username"] = userName;
            var dao = new UserDao();
            var temp = db.Users.FirstOrDefault(x => x.UserName == userName);
            if (temp.Role == 0)
            {
                ViewBag.Role = "User";
            }
            else
            {
                ViewBag.Role = "Admin";
            }
            var user = new CreateModel
            {
                ID = temp.ID,
                UserName = temp.UserName,
                Email = temp.Email,
                Password = temp.Password,
                UpdateAt = temp.UpdateAt
            };
            return View(user);
        }*/
        /* [HttpPost]
         public ActionResult Index(CreateModel model)
         {
             try
             {
                 if (ModelState.IsValid)
                 {
                     var dao = new UserDao();
                     var user = new User
                     {
                         ID = model.ID,
                         UserName = model.UserName,
                         Password = model.Password
                     };
                     dao.Update(user);
                     TempData["EditUserMessage"] = "Sửa thông tin thành công";
                     return RedirectToAction("Index", "ListUser");
                 }
                 return View("Index");
             }
             catch (Exception)
             {
                 throw;
             }
         }*/
    }

}
