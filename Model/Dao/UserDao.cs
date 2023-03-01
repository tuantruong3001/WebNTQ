using Model.EF;
using System;
using System.Collections.Generic;
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
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
        public int Register(string email, string user)
        {
            var emailExists = db.Users.SingleOrDefault(x => x.Email == email);
            var usernameExists = db.Users.SingleOrDefault(x => x.UserName == user);
            if (emailExists == null && usernameExists == null)
            {
                return 1; // Không có email hoặc username trong cơ sở dữ liệu
            }
            else if (emailExists == null)
            {
                return 0; // Không có email trong cơ sở dữ liệu
            }
            else
            {
                return -1; // Email đã tồn tại trong cơ sở dữ liệu
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
