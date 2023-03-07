using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        NtqDbContext db = null;
        public ProductDao()
        {
            db = new NtqDbContext();
        }
        public int Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        // cập nhật product
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                if (product != null)
                {
                    product.ProductName = entity.ProductName;
                    product.Slug = entity.Slug;
                    product.Detail = entity.Detail;
                    product.Price = entity.Price;
                    product.Trending = entity.Trending;
                    product.Path = entity.Path;
                    product.MetaKey = entity.MetaKey;
                    product.Status = true;
                    product.UpdateAt = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception) { return false; }
        }
        // xoá product, đổi trạng thái về false(done)
        public bool Delete(int id)
        {
            using (var context = new NtqDbContext())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    return false;
                }
                product.Status = false;
                product.DeleteAt = DateTime.Now;
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại sau", ex);
                }
            }
        }
        public Product GetByID(int id)
        {
            return db.Products.Find(id);
        }
        public Product GetByName(string name)
        {
            return db.Products.SingleOrDefault(x => x.ProductName == name);
        }
        //search theo detail & nameproduct
        public IEnumerable<Product> ListAllPaging(string searchString, bool roleFilter, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ProductName.Contains(searchString) ||  x.Detail.Contains(searchString));
                if (model == null)
                {
                    return null;
                }
            }
            if (roleFilter)
            {
                model = model.Where(x => x.Trending == true);
            }
           
            return model.OrderBy(x => x.NumberViews).ToPagedList(page, pageSize);
        }
    }
}
