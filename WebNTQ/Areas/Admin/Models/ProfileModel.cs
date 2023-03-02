using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNTQ.Areas.Admin.Models
{
    public class ProfileModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set;}
        public string UserName { get; set; }
        public int Role { get; set; }
    }
}