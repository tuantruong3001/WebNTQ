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
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var userDao = new UserDao();
            var loginResult = userDao.Login(model.Password, model.Email);

            switch (loginResult)
            {
                case 1:
                    var user = userDao.GetByEmail(model.Email);
                    var userSession = new UserLogin
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        UserID = user.ID
                    };
                    Session[CommonConstants.USER_SESSION] = userSession;
                    if (user.Role == 1)
                    {
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "HomeUser");
                    }

                case 0:
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                    break;

                case -1:
                    ModelState.AddModelError("", "Tài khoản bị xoá!");
                    break;

                case -2:
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                    break;

                default:
                    ModelState.AddModelError("", "Thông tin đăng nhập không đúng!");
                    break;
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            try
            {
                // Xóa session hiện tại
                Session.Clear();               
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã có lỗi xảy ra, vui lòng thử lại sau " + ex.Message;
                return View("Index");
            }
        }
    }
}