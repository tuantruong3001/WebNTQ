﻿using Model.Dao;
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
    public class HomeUserController : BaseController
    {
        // GET: Admin/HomeUser
        public ActionResult Index()
        {
            var userDao = new UserDao();
            var model = (UserLogin)Session[CommonConstants.USER_SESSION];
            var user = userDao.GetById(model.UserID);
            var profileModel = new ProfileModel
            {
                ID = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                UpdateAt = user.UpdateAt,
            };

            return View(profileModel);
        }
        [HttpPost]
        public ActionResult Index(ProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDao = new UserDao();
                    var userByEmail = userDao.GetByEmail(model.Email);
                    model.ID = userByEmail.ID;
                    bool isUserNameAvailable;

                    if (model.ConfirmPassword != model.Password)
                    {
                        ModelState.AddModelError("", "ConfirmPassword và Password không khớp");
                        return View("Index");
                    }

                    var userOld = userDao.GetById(model.ID);
                    if (model.UserName == userOld.UserName)
                    {
                        isUserNameAvailable = true;
                    }
                    else
                    {
                        isUserNameAvailable = userDao.CheckUserName(model.UserName);
                    }

                    if (isUserNameAvailable)
                    {
                        var user = new User
                        {
                            ID = model.ID,
                            Email = model.Email,
                            UserName = model.UserName,
                            Password = model.Password,
                            Role = model.Role
                        };
                        userDao.Update(user);
                        TempData["EditUserMessage"] = "Sửa thông tin thành công";
                        return RedirectToAction("Index", "HomeUser");
                    }

                    ModelState.AddModelError("", "UserName đã tồn tại");
                }

                return View(model);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                return View(model);
            }
        }

    }
}