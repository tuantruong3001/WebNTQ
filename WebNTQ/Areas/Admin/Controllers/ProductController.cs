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
        public ActionResult Create(ProductModel createModel, bool isTrending)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            try
            {
                var productDao = new ProductDao();
                var product = new Product
                {
                    ProductName = createModel.ProductName,
                    Slug = createModel.Slug,
                    Detail = createModel.Detail,
                    Price = createModel.Price,
                    Path = createModel.Path,
                    Trending = isTrending,
                    CreateAt = DateTime.Now,
                    Status = true,
                };
                productDao.Insert(product);
                TempData["UserMessage"] = "Thêm mới thông tin product thành công";
                return RedirectToAction("Index", "ListProduct");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                return View(createModel);
            }
        }
        // truyền product vào => sửa product
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
                Path = product.Path,
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
        public ActionResult Edit(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.TrendingOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "True", Text = "Top Trending", Selected = productModel.Trending == true },
                        new SelectListItem { Value = "False", Text = "None Trending", Selected = productModel.Trending == false },
                    };
                    var productDao = new ProductDao();
                    var product = new Product
                    {
                        ID = productModel.ID,
                        ProductName = productModel.ProductName,
                        Slug = productModel.Slug,
                        Detail = productModel.Detail,
                        Trending = productModel.Trending,
                        Path = productModel.Path,
                        Price = productModel.Price,
                        UpdateAt = productModel.UpdateAt,
                        Status = true,
                    };
                    productDao.Update(product);
                    TempData["EditUserMessage"] = "Sửa thông tin product thành công";
                    return RedirectToAction("Index", "ListProduct");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Đã có lỗi xảy ra, vui lòng thử lại sau: {ex.Message}");
                    return View(productModel);
                }
            }
            return View(productModel);
        }
        // xoá product
        public ActionResult Delete(int id)
        {
            try
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
            catch (Exception)
            {
                ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại sau!");
                return View("Index"); 
            }

        }
    }
}