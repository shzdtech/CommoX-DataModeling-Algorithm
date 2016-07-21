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
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<EnterpriseState> EnterpriseStates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementType> RequirementTypes { get; set; }
        public DbSet<RequirementState> RequirementStates { get; set; }
        public DbSet<RequirementDetail> RequirementDetails { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Trade> Trades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(@"Server=(114.55.54.144)\mssqllocaldb;Database=master;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Server=114.55.54.144; UID=sa; password=shzdtech!123; database=master;");
        }


    }

    

    public class Requirement
    {
        [Key]
        public int RequirementId { get; set; }
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public int ProductId { get; set; }
        public int RequirementTypeId { get; set; }
        public int ProductPrice { get; set; }
        public decimal ProductQuota { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int RequirementStateId { get; set; }

    }
    public class RequirementType
    {
        [Key]
        public int RequirementTypeId { get; set; }
        public string RequirementTypeValue { get; set; }


    }
    public class RequirementState
    {
        [Key]
        public int RequirementStateId { get; set; }
        public int RequirementStateValue { get; set; }
    }

    public class RequirementDetail
    {
        public int RequirementId { get; set; }
        public int FilterId { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class Filter
    {
        [Key]
        public int FilterId { get; set; }
        public string FilterKey { get; set; }
        public int OperationId { get; set; }
        public string FilterValue { get; set; }
        public int StateId { get; set; }
    }
    


}
