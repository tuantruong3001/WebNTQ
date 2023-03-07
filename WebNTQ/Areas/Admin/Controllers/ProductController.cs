using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNTQ.Areas.Admin.Models;

namespace WebNTQ.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductModel createmodel, bool Trending)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            try
            {
                var dao = new ProductDao();
                var product = new Product
                {
                    ProductName = createmodel.ProductName,
                    Slug = createmodel.Slug,
                    Detail = createmodel.Detail,
                    Price = createmodel.Price,
                    Trending = Trending,
                    CreateAt = DateTime.Now,
                    Status = true,
                };
                dao.Insert(product);
                TempData["UserMessage"] = "Thêm mới thông tin product thành công";
                return RedirectToAction("Index", "ListProduct");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                return View(createmodel);
            }
        }
        // sửa product
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productDao = new ProductDao();
            var product = productDao.GetByID(id);

            var productModel = new ProductModel
            {
                ID = product.ID,
                ProductName = product.ProductName,
                Slug = product.Slug,
                Detail = product.Detail,
                Trending = product.Trending,
                Price = product.Price,
                UpdateAt = product.UpdateAt
            };
            ViewBag.TrendingOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "True", Text = "Top Trending", Selected = product.Trending == true },
                new SelectListItem { Value = "False", Text = "None Trending", Selected = product.Trending == false },
            };
            return View(productModel);
        }
        [HttpPost]
        public ActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.TrendingOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "True", Text = "Top Trending", Selected = model.Trending == true },
                        new SelectListItem { Value = "False", Text = "None Trending", Selected = model.Trending == false },
                    };
                    var productDao = new ProductDao();
                    var product = new Product
                    {
                        ID = model.ID,
                        ProductName = model.ProductName,
                        Slug = model.Slug,
                        Detail = model.Detail,
                        Trending = model.Trending,
                        Price = model.Price,
                        UpdateAt = model.UpdateAt,
                        Status = true,
                    };
                    productDao.Update(product);
                    TempData["EditUserMessage"] = "Sửa thông tin product thành công";
                    return RedirectToAction("Index", "ListProduct");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                    return View(model);
                }
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            ProductDao productDao = new ProductDao();
            bool success = productDao.Delete(id);
            if (success)
            {
                TempData["DeleteUserMessage"] = "Xoá thành công";
            }
            else
            {
                TempData["DeleteUserMessage"] = "Xoá không thành công";
            }
            return RedirectToAction("Index", "ListProduct");
        }
    }
}