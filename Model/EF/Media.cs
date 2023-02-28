namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medias")]
    public partial class Media
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        [StringLength(150)]
        public string Path { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public virtual Product Product { get; set; }
    }
}
