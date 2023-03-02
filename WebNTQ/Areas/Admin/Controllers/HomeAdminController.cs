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
    public class HomeAdminController : BaseController
    {
        [HttpGet]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        /*[HttpPost]
        public ActionResult Update(ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(profileModel.Password, profileModel.Email);
                if (result == 1)
                {
                    var user = dao.GetByEmail(profileModel.Email);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.Email = user.Email;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);                  
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị xoá!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!");
                }
            }
            return View("Index");
        }*/
    }
}