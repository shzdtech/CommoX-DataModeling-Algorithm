using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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


    public class Enterprise
    {
        [Key]
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string RegisterNumber { get; set; }
        public DateTime RegisterTime { get; set; }
        public int RegisterCapital { get; set; }
        public string RegisterAddress { get; set; }
        public string LegalRepresentative { get; set; }
        public int InvoicedQuantity { get; set; }
        public int BusinessTypeId { get; set; }
        public string BusinessRange { get; set; }
        public int ReputationGrade { get; set; }
        public string AnnualInspection { get; set; }
        public double PreviousSales { get; set; }
        public double PreviousProfit { get; set; }
        public string PaymentMethodId { get; set; }
        public string RegisterBank { get; set; }
        public string RegisterAccount { get; set; }
        public DateTime CreateTime { get; set; }
        public int StateId { get; set; }


    }

    public class BusinessType
    {
        [Key]
        public int BusinessTypeId { get; set; }
        public string BusinessTypeName { get; set; }
        public int ParentId { get; set; }
        public int StateId { get; set; }

    }

    public class EnterpriseState
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public int StateId { get; set; }
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public decimal LimitedQuota { get; set; }
        public int StateId { get; set; }
    }

    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int ParentId { get; set; }
        public int StateId { get; set; }
    }


    public class EnterpriseProduct
    {
        public int EnterpriseId { get; set; }
        public int ProductId { get; set; }
        public string StorageAddress { get; set; }
        public string WarehouseReceipt { get; set; }
        public string CreateTime { get; set; }
    }

    public class AcceptanceBill
    {
        public int AcceptanceBillId { get; set; }
        public string BankId { get; set; }
        public int EnterpriseId { get; set; }
        public string DrawerName { get; set; }
        public string DrawerAccount { get; set; }
        public string DrawerBankId { get; set; }
        public string PayeeName { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeBankId { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string AgreementNumber { get; set; }

    }

    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
    }
         

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int EnterpriseId { get; set; }
        public int StateId { get; set; }
        public DateTime LastLoginTime { get; set; }
    }

    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
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

    public class Trade
    {
        [Key]
        public int TradeId { get; set; }
        public int RequirementId { get; set; }
        public int RequirementType { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Quota { get; set; }
        public decimal TotalPrice { get; set; }
        public int FromRequirementId { get; set; }
        public DateTime CreateTime { get; set; }

    }


}
