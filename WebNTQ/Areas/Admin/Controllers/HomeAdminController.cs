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
    public class HomeAdminController : BaseController
    {
        [HttpGet]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index(int id)
        {
            var dao = new UserDao();
            var temp = dao.GetByID(id);
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
        }
        [HttpPost]
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
        }
    }

}
