using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WebNTQ.Areas.Admin.Models;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateModel createmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var dao = new UserDao();
            var result = dao.RegisterCheck(createmodel.Email, createmodel.UserName);

            switch (result)
            {
                case -1:
                    ModelState.AddModelError("", "UserName đã tồn tại!");
                    break;
                case 0:
                    ModelState.AddModelError("", "Email đã tồn tại!");
                    break;
                case 1:
                    var user = new User
                    {
                        UserName = createmodel.UserName,
                        Email = createmodel.Email,
                        Password = createmodel.Password,
                        Role = 0,
                        CreateAt = DateTime.Now,
                        Status = true,
                    };
                    dao.Insert(user);
                    TempData["UserMessage"] = "Thêm mới thông tin user thành công";
                    return RedirectToAction("Index", "ListUser");
                default:
                    ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại sau!");
                    break;
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
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
        public ActionResult Edit(CreateModel model)
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
                    TempData["EditUserMessage"] = "Update thông tin user thành công";
                    return RedirectToAction("Index", "ListUser");
                }
                return View("Edit");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}