using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class ProductUserController : Controller
    {
        // GET: Admin/ProductUser
        public ActionResult Index(string searchString, bool roleFiler = false, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, roleFiler, page, pageSize);
            if (roleFiler)
            {
                model = model.Where(x => x.Trending == true).ToPagedList(page, pageSize);
            }
            ViewBag.SearchString = searchString;
            ViewBag.Trending = roleFiler;
            return View(model);
        }
    }
}