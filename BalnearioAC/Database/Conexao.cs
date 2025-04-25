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
        public DbSet<Models.ReportReservation> ReportReservations { get; set; }
        public DbSet<Models.ReportSalesByEmployee> ReportSalesByEmployee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeId)
                .HasConstraintName("FK_Sale_Employee");

            // Relacionamentos já existentes
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Visitor)
                .WithMany()
                .HasForeignKey(r => r.VisitorId)
                .HasConstraintName("FK_Reservation_Visitor");

            modelBuilder.Entity<ReportReservation>()
                .HasOne(r => r.Kiosk)
                .WithMany()
                .HasForeignKey(r => r.KioskId)
                .HasConstraintName("FK_ReportReservation_Kiosk");

            modelBuilder.Entity<ReportReservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .HasConstraintName("FK_ReportReservation_User");

            modelBuilder.Entity<ItemSale>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .HasConstraintName("FK_ItemSale_Product");

            modelBuilder.Entity<ItemSale>()
                .HasOne(i => i.Sale)
                .WithMany(s => s.ItemSales)
                .HasForeignKey(i => i.SaleId)
                .HasConstraintName("FK_ItemSale_Sale");

            modelBuilder.Entity<ItemSale>()
                .HasOne(i => i.Product)
                .WithMany(p => p.ItemSales)          // <- use a mesma navegação do outro lado
                .HasForeignKey(i => i.ProductId)     // <- FK única
                .HasConstraintName("FK_ItemSale_Product");


            base.OnModelCreating(modelBuilder);
        }

    }
}
