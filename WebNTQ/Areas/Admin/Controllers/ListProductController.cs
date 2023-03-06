using Model.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class ListProductController : BaseController
    {
        // GET: Admin/ListProduct       
        private readonly ProductDao _dao = new ProductDao();

        public ActionResult Index(string searchString,bool roleFilter = false , int page = 1, int pageSize = 15)
        {
            var model = _dao.ListAllPaging(searchString, roleFilter, page, pageSize);

            if (roleFilter)
            {
                model = model.Where(x => x.Trending == true).ToPagedList(page, pageSize);
            }
           
            ViewBag.SearchString = searchString;
            ViewBag.RoleFilter = roleFilter;
     
            return View(model);
        }
       
    }
}