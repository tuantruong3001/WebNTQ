namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
