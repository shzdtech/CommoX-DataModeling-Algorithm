using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Micro.Future.Business.DataAccess.Commo
{
    public class CommoXContext : DbContext
    {
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<EnterpriseState> EnterpriseStates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestState> RequestStates { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Trade> Trades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        }
    }


    public class Enterprise
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string RegisterNumber { get; set; }
        public DateTime RegisterTime { get; set; }
        public int RegisterCapital { get; set; }
        public string RegisterAddress { get; set; }
        public string LegalRepresentative { get; set; }
        public int BusinessTypeId { get; set; }
        public string BusinessRange { get; set; }
        public int Reputation { get; set; }
        public DateTime CreateTime { get; set; }
        public int StateId { get; set; }


    }

    public class BusinessType
    {
        public int BusinessTypeId { get; set; }
        public string BusinessTypeName { get; set; }
        public int ParentId { get; set; }
        public int StateId { get; set; }

    }

    public class EnterpriseState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }



    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int EnterpriseId { get; set; }

    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Request
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public int ProductId { get; set; }
        public int RequestTypeId { get; set; }
        public int ProductPrice { get; set; }
        public decimal ProductQuota { get; set; }
        public int RequestDetailId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int RequestStateId { get; set; }

    }
    public class RequestType
    {
        public int RequestTypeId { get; set; }
        public string RequestTypeValue { get; set; }


    }
    public class RequestState
    {
        public int RequestStateId { get; set; }
        public int RequestStateValue { get; set; }
    }

    public class RequestDetail
    {
        public int RequestDetailId { get; set; }
        public int FilterId { get; set; }
        public DateTime CreateTime { get; set; }


    }

    public class Filter
    {
        public int FilterId { get; set; }
        public string FilterKey { get; set; }
        public string OperationId { get; set; }
        public string FilterValue { get; set; }
        public int StateId { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal LimitedQuota { get; set; }
        public int StateId { get; set; }
    }

    public class Trade
    {
        public int TradeId { get; set; }
        public int RequestId { get; set; }
        public int RequestType { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Quota { get; set; }
        public decimal TotalPrice { get; set; }
        public int FromRequestId { get; set; }
        public DateTime CreateTime { get; set; }

    }


}
