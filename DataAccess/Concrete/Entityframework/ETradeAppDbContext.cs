using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entityframework
{
    public class ETradeAppDbContext : DbContext
    {
        //parametreli constructor oluştururuz.DbContextOptions<CallCenterDbContext> türünde bir parametre almalı ve bu parametreyi DbContext'in temel yapıcısına aktarmalıdır.
        // eğer DbContext IoC den gelecekse bu mecburen olacak
        public ETradeAppDbContext(DbContextOptions<ETradeAppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(a => a.User)
                .WithMany(a => a.Products)           //Bir kullanıcının birden çok ürünü olabilir
                .HasForeignKey(a => a.UserId);              

            modelBuilder.Entity<Product>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().HasKey(a => a.Id);

            //Seed Data
            //Migrate edince başlangıçta oluşacak test datalar 
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductName = "Laptop", Price = 10000, Description = "Apple 2023 Model", UserId = 1 },
                new Product { Id = 2, ProductName = "CepTelefonu", Price = 8000, Description = "Samsung 2023 Model", UserId = 1 });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Esra", LastName = "Yaşın", Email = "esra@gmail.com", Password = "esra1234" },
                new User { Id = 2, FirstName = "Ahmet", LastName = "Benk", Email = "ahmet@gmail.com", Password = "ahmet1234" },
                new User { Id = 3, FirstName = "Mehmet", LastName = "Mutlu", Email = "mehmet@gmail.com", Password = "mehmet1234" });
        }
    }
}



