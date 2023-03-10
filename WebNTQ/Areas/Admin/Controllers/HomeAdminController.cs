using Microsoft.Ajax.Utilities;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using WebNTQ.Areas.Admin.Models;
using WebNTQ.Common;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseController
    {

        // GET: Admin/Home

        public ActionResult Index()
        {
            try
            {
                var dao = new UserDao();
                var model = (UserLogin)Session[CommonConstants.USER_SESSION];
                var user = dao.GetByID(model.UserID);
                var result = new ProfileModel
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role,
                    UpdateAt = user.UpdateAt,
                };

                return View(result);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(ProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    var result = dao.GetByEmail(model.Email);
                    model.ID = result.ID;
                    bool checkUserName;
                    if (model.ConfirmPassword != model.Password)
                    {
                        ModelState.AddModelError("", "ConfirmPassword và Password không khớp");
                        return View("Index");
                    }
                    var userOld = dao.GetByID(model.ID);
                    if (model.UserName == userOld.UserName) checkUserName = true;
                    else checkUserName = dao.CheckUserName(model.UserName);
                    if (checkUserName)
                    {
                        var user = new User
                        {
                            ID = model.ID,
                            Email = model.Email,
                            UserName = model.UserName,
                            Password = model.Password,
                            Role = model.Role,

                        };
                        dao.Update(user);
                        TempData["EditUserMessage"] = "Sửa thông tin thành công";
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    if (!checkUserName) { ModelState.AddModelError("", "UserName đã tồn tại"); };
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
