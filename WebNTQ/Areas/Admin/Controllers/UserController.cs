using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                        CreateAt = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture),
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
            var userDao = new UserDao();
            var user = userDao.GetByID(id);

            string role = (user.Role == 0) ? "User" : "Admin";
            ViewBag.Role = role;

            var userModel = new CreateModel
            {
                ID = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                UpdateAt = user.UpdateAt
            };
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Edit(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDao = new UserDao();
                    var user = new User
                    {
                        ID = model.ID,
                        UserName = model.UserName,
                        Password = model.Password
                    };
                    userDao.Update(user);
                    TempData["EditUserMessage"] = "Update thông tin user thành công";
                    return RedirectToAction("Index", "ListUser");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                    return View(model);
                }
            }
            return View(model);
        }
        // chưa xong
        public ActionResult Delete(int id)
        {
            UserDao userDao = new UserDao();
            bool success = userDao.Delete(id);
            if (success)
            {
                TempData["DeleteUserMessage"] = "Xoá thành công";
            }
            else
            {
                TempData["DeleteUserMessage"] = "Xoá không thành công";
            }
            return RedirectToAction("Index", "ListUser");
        }
        
    }
}