using Data_Assess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Assess_Layer
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
            CarDetails = Set<CarDetails>();
            User = Set<User>();
            RentalAgreement = Set<RentalAgreement>();
        }

        public DbSet<CarDetails> CarDetails { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RentalAgreement> RentalAgreement { get; set; }
        public DbSet<AdminLogin> AdminLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalAgreement>()
                .HasOne(ra => ra.User)
                .WithMany(u => u.RentalAgreements)
                .HasForeignKey(ra => ra.UserId);

            modelBuilder.Entity<RentalAgreement>()
                .HasOne(ra => ra.CarDetails)
                .WithMany(cd => cd.RentalAgreements)
                .HasForeignKey(ra => ra.Vehicle_Id);
        }
    }
}
