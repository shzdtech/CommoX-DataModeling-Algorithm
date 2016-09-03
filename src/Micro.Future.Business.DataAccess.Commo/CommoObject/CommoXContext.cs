using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo
{
    public class CommoXContext : DbContext
    {
        public DbSet<AcceptanceBill> AcceptanceBills { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<EnterpriseProduct> EnterpriseProducts { get; set; }
        public DbSet<EnterpriseState> EnterpriseStates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementState> RequirementStates { get; set; }
        public DbSet<RequirementType> RequirementTypes { get; set; }
        public DbSet<RequirementRule> RequirementRules { get; set; }
        public DbSet<RequirementRuleOperation> RequirementRuleOperations { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeChain> TradeChains { get; set; }
        public DbSet<User> Users { get; set; }

        public CommoXContext(DbContextOptions<CommoXContext> options) : base(options)
        {

        }
 


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // optionsBuilder.UseSqlServer(@"Server=(114.55.54.144)\mssqllocaldb;Database=master;Trusted_Connection=True;");
        //    //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        //    optionsBuilder.UseSqlServer(@"Server=114.55.54.144; User Id=sa;; Password=shzdtech!123; Database=Commo;");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Trade>()
        //        .HasMany(b => b.Orders)
        //        .WithOne();
        //}
    }

    
    


}
