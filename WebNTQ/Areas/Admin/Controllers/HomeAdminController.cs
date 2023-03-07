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
