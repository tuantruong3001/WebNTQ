using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        NtqDbContext db = null;
        public UserDao()
        {
            db = new NtqDbContext();
        }
        // thêm mới
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        // cập nhật
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                if (user != null)
                {
                    user.UserName = entity.UserName;
                    user.Password = entity.Password;
                    user.UpdateAt = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    db.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception) { return false; }
        }
        //update profile(doing)
        public bool UpdateProfile(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.UpdateAt = DateTime.Now;
                db.SaveChanges();
                return true;

            }
            catch (Exception) { return false; }
        }

        // xoá uer, đổi trạng thái về false
        public bool Delete(int id)
        {
            using (var context = new NtqDbContext())
            {
                var user = context.Users.Find(id);
                if (user == null)
                {
                    return false;
                }
                user.Status = false;
                user.DeleteAt = DateTime.ParseExact(DateTime.Now.Date.ToString("dd/MM/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
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

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User GetByID(int id)
        {
            return db.Users.Find(id);
        }      
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
        //check khi thêm mới user or đk
        public int RegisterCheck(string email, string user)
        {

            var emailExists = db.Users.SingleOrDefault(x => x.Email == email);
            var usernameExists = db.Users.SingleOrDefault(x => x.UserName == user);
            if (emailExists == null && usernameExists == null)
            {
                return 1;
            }
            else if (emailExists != null)
            {
                return 0; // trùng email
            }
            else
            {
                return -1; // trùng user
            }
        }
        //đăng nhập
        public int Login(string passWord, string email)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if (result == null) return 0; // tài khoản ko tồn tại
            if (result.Status == false) return -1; // tài khoản bị xoá
            if (result.Password != passWord) return -2; // sai mật khẩu
            return 1;
        }
        //list page
        public IEnumerable<User> ListAllPaging(string searchString, bool roleFilter, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Email.Contains(searchString));
                if (model == null)
                {
                    return null;
                }
            }
            if (roleFilter)
            {
                model = model.Where(x => x.Role == 1);
            }
            return model.OrderBy(x => x.CreateAt).ToPagedList(page, pageSize);
        }
    }
}
