using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNTQ.Areas.Admin.Models
{
    public class ProductModel
    {
        [RegularExpression(@"^.{2,150}$", ErrorMessage = "{0} chỉ từ 2 đến 150 ký tự.")]
        [Required(ErrorMessage = "{0} không được để trống!")]
        
        public string Slug { get; set; }
        
        [RegularExpression(@"^.{2,50}$", ErrorMessage = "{0} chỉ từ 2 đến 50 ký tự.")]
        [Required(ErrorMessage = "{0} không được để trống!")]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string Detail { get; set; }
        public int ID { get; set; }

        [StringLength(150)]
        public string Path { get; set; }
        public bool? Trending { get; set; }

        public double? NumberViews { get; set; }
        [Required(ErrorMessage = "{0} không được để trống!")]
        public double? Price { get; set; }
        public bool? Status { get; set; }
        [StringLength(50)]
        public string MetaKey { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreateAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdateAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DeleteAt { get; set; }
    }
}