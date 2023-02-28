using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class NtqDbContext : DbContext
    {
        public NtqDbContext()
            : base("name=NtqDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaKey)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<WishList>()
                .Property(e => e.Title)
                .IsUnicode(false);
        }
    }
}
