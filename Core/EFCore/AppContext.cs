using Core.EFCore.Models;
using Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EFCore
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Car_DAL> Cars { get; set; }

        public DbSet<CarStation_DAL> CarStations { get; set; }

        public DbSet<User_DAL> Users { get; set; }

        public DbSet<Order_DAL> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car_DAL>().ToTable("Cars");
            modelBuilder.Entity<CarStation_DAL>().ToTable("CarStations");
            modelBuilder.Entity<User_DAL>().ToTable("Users");
            modelBuilder.Entity<Order_DAL>().ToTable("Orders");

            //modelBuilder.Entity<Car_DAL>()
            //.HasMany((b))

            //.WithMany<User_DAL>(b => b.)

            modelBuilder.Entity<Car_DAL>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<CarStation_DAL>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<User_DAL>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Order_DAL>()
                .HasKey(b => b.Id);

            //modelBuilder.Entity<Car_DAL>()
            //    .Property(b => b.Id)
            //    .UseIdentityColumn();

            //modelBuilder.Entity<CarStation_DAL>()
            //    .Property(b => b.Id)
            //    .UseIdentityColumn();

            //modelBuilder.Entity<User_DAL>()
            //    .Property(b => b.Id)
            //    .UseIdentityColumn();

            //modelBuilder.Entity<Order_DAL>()
            //    .Property(b => b.Id)
            //    .UseIdentityColumn();

            modelBuilder.Entity<CarStation_DAL>()
                .Property(b => b.TypeOfWork)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<WorkType, int>>(v));

            modelBuilder.Entity<Order_DAL>()
               .Property(b => b.CompletedWork)
               .HasConversion(
                   v => JsonConvert.SerializeObject(v),
                   v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v));
        }
    }
}
