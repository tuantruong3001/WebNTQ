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
    public class RegisterController : Controller
    {
        // GET: Admin/Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(RegisterModel registerModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var dao = new UserDao();
            var result = dao.RegisterCheck(registerModel.Email, registerModel.UserName);

            if (registerModel.ConfirmPassword != registerModel.Password)
            {
                ModelState.AddModelError("", "ConfirmPassword phải trùng với Password");
                return View("Index");
            }

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
                        UserName = registerModel.UserName,
                        Email = registerModel.Email,
                        Password = registerModel.Password,
                        Role = 0,
                        CreateAt = DateTime.Now,
                        Status = true,
                    };
                    dao.Insert(user);
                    TempData["SuccessMessage"] = "Đăng ký thành công";
                    return RedirectToAction("Index", "Login");
            }
            return View("Index");
        }
    }
}