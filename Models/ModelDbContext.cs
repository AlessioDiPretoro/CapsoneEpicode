using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Stones.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<DetailOrder> DetailOrder { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductSubject> ProductSubject { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailOrder>()
                .Property(e => e.priceCad)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.prov)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.DetailOrder)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.idOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.DetailOrder)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.idProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Post)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.idProduct);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.ProductCategory)
                .HasForeignKey(e => e.idCategory);

            modelBuilder.Entity<ProductSubject>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.ProductSubject)
                .HasForeignKey(e => e.idSubject);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.idBuyer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.idUser)
                .WillCascadeOnDelete(false);
        }
    }
}
