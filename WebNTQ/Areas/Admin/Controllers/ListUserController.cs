using Model.Dao;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class ListUserController : BaseController
    {
        // GET: Admin/ListUser
        private readonly UserDao _dao = new UserDao();

        public ActionResult Index(string searchString, bool active = false, bool deleted = false, bool roleFilter = false  , int page = 1, int pageSize = 10)
        {
            var model = _dao.ListAllPaging(searchString, active, deleted,  roleFilter, page, pageSize);

            if (roleFilter)
            {
                model = model.Where(x => x.Role == 1).ToPagedList(page, pageSize);
            }
            if (active)
            {
                model = model.Where(x => x.Status == true).ToPagedList(page, pageSize);
            }
            if (deleted)
            {
                model = model.Where(x => x.Status == false).ToPagedList(page, pageSize);
            }
            ViewBag.SearchString = searchString;
            ViewBag.RoleFilter = roleFilter;
            ViewBag.StatusActive = active;
            ViewBag.StatusDeleted = deleted;
            return View(model);
        }

    }
}