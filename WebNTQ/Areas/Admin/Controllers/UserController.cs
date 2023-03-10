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
        public ActionResult Create(CreateModel createModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var userDao = new UserDao();
                var result = userDao.RegisterCheck(createModel.Email, createModel.UserName);

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
                            UserName = createModel.UserName,
                            Email = createModel.Email,
                            Password = createModel.Password,
                            Role = 0,
                            CreateAt = DateTime.Now,
                            Status = true,
                        };
                        userDao.Insert(user);
                        TempData["UserMessage"] = "Thêm mới thông tin user thành công";
                        return RedirectToAction("Index", "ListUser");
                    default:
                        ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại sau!");
                        break;
                }

                return View("Index");
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại sau!");
                return View("Index");
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var userDao = new UserDao();
                var user = userDao.GetById(id);

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
            catch (Exception)
            {
                return View("Index");
            }

        }
        [HttpPost]
        public ActionResult Edit(CreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDao = new UserDao();
                    bool checkUserName = userDao.CheckUserName(model.UserName);
                    bool checkEmail = userDao.CheckEmail(model.Email);
                    var userOld = userDao.GetById(model.ID);

                    if (model.UserName == userOld.UserName) checkUserName = true;
                    if (model.Email == userOld.Email) checkEmail = true;
                    if (checkUserName && checkEmail)
                    {
                        var user = new User
                        {
                            ID = model.ID,
                            Email = model.Email,
                            UserName = model.UserName,
                            Password = model.Password,
                            Status = model.Status,
                        };
                        userDao.Update(user);
                        TempData["EditUserMessage"] = "Update thông tin user thành công";
                        return RedirectToAction("Index", "ListUser");
                    }
                    if (!checkUserName) { ModelState.AddModelError("", "Username đã tồn tại"); };
                    if (!checkEmail) { ModelState.AddModelError("", "Email đã tồn tại"); };
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                return View(model);
            }
        }
        //xoá user, đổi status => false
        public ActionResult Delete(int id)
        {
            try
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
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                return RedirectToAction("Index", "ListUser");
            }

        }

    }
}