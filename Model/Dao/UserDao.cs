using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                    user.UpdateAt = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { return false; }

        }
        public User GetByID(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
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
        public int Login(string passWord, string email)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if (result == null) return 0; // tài khoản ko tồn tại
            if (result.Status == false) return -1; // tài khoản bị xoá
            if (result.Password != passWord) return -2; // sai mật khẩu
            return 1;
        }
    }
}
