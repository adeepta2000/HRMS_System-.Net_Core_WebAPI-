using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRMSDevelopment.Model.DBEntity;

namespace HRMSDevelopment.Model
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRole(builder);
        }

        private static void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                  new IdentityRole() { Name = "HR", ConcurrencyStamp = "2", NormalizedName = "HR" },
                  new IdentityRole() { Name = "Employee", ConcurrencyStamp = "3", NormalizedName = "Employee" }

                );
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<SalaryRevision> SalaryRevisions { get; set; }
        public DbSet<Deduction> Deductions { get; set;}
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<PayrollRecord> PayrollRecords { get; set; }
        public DbSet<Tax> Taxs { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<BonusCriteria> BonusCriterias { get; set; }
        public DbSet<BonusDistribution> BonusDistributions { get; set; }
    }
}
