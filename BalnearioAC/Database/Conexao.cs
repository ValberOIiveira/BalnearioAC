using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BalnearioAC.Models;

namespace BalnearioAC.Database
{

    public class Conexao : DbContext
    {
        public Conexao(DbContextOptions<Conexao> options) : base(options)
        {

        }

        public DbSet<Models.User> Users { get; set; }   
        public DbSet<Models.UserType> UserTypes { get; set; }
        public DbSet<Models.Visitor> Visitors { get; set; }
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Kiosk> Kiosks { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Reservation> Reservations { get; set; }
        public DbSet<Models.Sale> Sales { get; set; }
        public DbSet<Models.LogReservation> LogReservations { get; set; }
        public DbSet<Models.LogSale> LogSales { get; set; }
        public DbSet<Models.LogUserActivity> LogUserActivities { get; set; }
        public DbSet<Models.Report> Reports { get; set; }
        public DbSet<Models.ItemSale> ItemSales { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Visitor)  
                .WithMany()
                .HasForeignKey(r => r.VisitorId)  
                .HasConstraintName("FK_Reservation_Visitor");

            base.OnModelCreating(modelBuilder);
        }
        


    }
}