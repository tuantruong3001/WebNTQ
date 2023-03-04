using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNTQ.Areas.Admin.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} chỉ từ 10 đến 30 ký tự.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName không được để trống!")]
        [RegularExpression(@"^.{2,10}$", ErrorMessage = "{0} chỉ từ 2 đến 10 ký tự.")]

        public string UserName { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,20}).*$", ErrorMessage = "{0} phải từ 8-20 kí tự bao gồm ít nhất 1 kí tự số, 1 kí tự viết hoa và 1 kí tự đặc biệt.")]       
        [Required(ErrorMessage = "Password không được để trống!")]
        public string Password { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,20}).*$", ErrorMessage = "{0} phải từ 8-20 kí tự bao gồm ít nhất 1 kí tự số, 1 kí tự viết hoa và 1 kí tự đặc biệt.")]
        [Required(ErrorMessage = "ConfirmPassword không được để trống!")]
        public string ConfirmPassword { get; set; }
        public int ID { get; set; }
        public int? Role { get; set; }
        public bool? Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreateAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdateAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DeleteAt { get; set; }

    }
}