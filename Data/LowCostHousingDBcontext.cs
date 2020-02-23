using LowCostHousing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LowCostHousing.Data
{
    public class LowCostHousingDBcontext:IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-O68M5NP;Initial Catalog=LowCostHousing;User ID=root;Password=root12345;Trusted_Connection=True");
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-O68M5NP;Initial Catalog=LowCostHousing;User ID=sa;Password=sa12345;Trusted_Connection=True");
        }

        public DbSet<ProjectMaster> ProjectMaster { get; set; }
        public DbSet<ClientRegistration> ClientRegistration { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Suburb> Suburb { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<PaymentPlan> PaymentPlans { get; set; }

        public DbSet<PaymentPlanAmount> PaymentPlanAmounts { get; set; }
    }
}
