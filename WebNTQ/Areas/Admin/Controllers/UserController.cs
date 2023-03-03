using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WebNTQ.Areas.Admin.Models;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(CreateModel createmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var dao = new UserDao();
            var result = dao.RegisterCheck(createmodel.Email, createmodel.UserName);

            switch (result)
            {
                case -1:
                    ModelState.AddModelError("", "UserName đã tồn tại!");
                    break;
                case 0:
                    ModelState.AddModelError("", "Email đã tồn tại!");
                    break;
                case 1:
                    var user = new User
                    {
                        UserName = createmodel.UserName,
                        Email = createmodel.Email,
                        Password = createmodel.Password,
                        Role = 0,
                        CreateAt = DateTime.Now,
                        Status = true,
                    };
                    dao.Insert(user);
                    TempData["UserMessage"] = "Thêm mới thông tin user thành công";
                    return View("Index");
                default:
                    ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại sau!");
                    break;
            }
            return View("Index");
        }

    }
}