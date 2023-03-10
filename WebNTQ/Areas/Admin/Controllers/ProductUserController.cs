using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebNTQ.Common;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class ProductUserController : BaseController
    {
        // GET: Admin/ProductUser
        public ActionResult Index(string searchString, bool roleFiler = false, int page = 1, int pageSize = 8)
        {
            try
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
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }
        public ActionResult Detail(int id)
        {
            try
            {
                var dao = new ProductDao();
                var product = dao.ViewDetail(id);
                var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
                if (sessionUser != null) { ViewBag.UserID = sessionUser.UserID; }
                return View(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}