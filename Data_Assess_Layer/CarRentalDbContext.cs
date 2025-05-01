using Data_Assess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SigningKey> SigningKeys { get; set; }

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
            modelBuilder.Entity<UserRole>()
               .HasKey(ur => new {
                   ur.UserId,
                   ur.RoleId
               });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Users)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                        .HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Admin Role" },
                new Role { Id = 2, Name = "Editor", Description = " Editor Role" },
                new Role { Id = 3, Name = "User", Description = "User Role" }
            );
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    ClientId = "Client1",
                    Name = "Client Application 1",
                    ClientURL = "https://client1.com"
                },
                new Client
                {
                    Id = 2,
                    ClientId = "Client2",
                    Name = "Client Application 2",
                    ClientURL = "https://client2.com"
                }
            );
        }

    }
}
