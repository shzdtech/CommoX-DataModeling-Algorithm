﻿using System;
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
        public DbSet<RequirementFilter> Filters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Trade> Trades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=(114.55.54.144)\mssqllocaldb;Database=master;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Server=114.55.54.144; User Id=sa;; Password=shzdtech!123; Database=master;");
        }


    }

    
    


}
