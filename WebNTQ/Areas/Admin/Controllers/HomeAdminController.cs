using Microsoft.Ajax.Utilities;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var session = (WebNTQ.Common.UserLogin)Session[WebNTQ.Common.CommonConstants.USER_SESSION];
            var user = dao.GetByID(1);          

            return View();
        }  
    }

}
