using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNTQ.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="UserName không được để trống!")]
        public string UserName { get; set;}

        [Required(ErrorMessage = "Password không được để trống!")]
        public string Password { get; set;}
        public bool RememberMe { get; set;}

    }
}