using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNTQ.Areas.Admin.Models;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class HomeUserController : BaseController
    {
        // GET: Admin/HomeUser
        public ActionResult Index()
        {
            var dao = new UserDao();
            var model = (WebNTQ.Common.UserLogin)Session[WebNTQ.Common.CommonConstants.USER_SESSION];
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

    }
}