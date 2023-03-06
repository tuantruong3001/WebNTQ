namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Medias = new HashSet<Media>();
            Reviews = new HashSet<Review>();
            WishLists = new HashSet<WishList>();
        }

        public int ID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(150)]
        public string Slug { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(150)]
        public string Path { get; set; }

        [StringLength(50)]
        public string Detail { get; set; }

        public bool? Trending { get; set; }

        public bool? Status { get; set; }

        public double? NumberViews { get; set; }

        public double? Price { get; set; }

        [StringLength(50)]
        public string MetaKey { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Media> Medias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
