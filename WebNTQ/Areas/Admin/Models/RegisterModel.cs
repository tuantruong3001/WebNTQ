﻿using System;
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

        [RegularExpression(@"^.{8,20}$", ErrorMessage = "{0} chỉ từ 8 đến 20 ký tự.")]
        [Required(ErrorMessage = "Password không được để trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword không được để trống!")]
        public string ConfirmPassword { get; set; }

    }
}