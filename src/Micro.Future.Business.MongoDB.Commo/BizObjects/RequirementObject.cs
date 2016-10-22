using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Micro.Future.Business.MongoDB.Commo.BizObjects
{
    /// <summary>
    /// 需求对象
    /// </summary>
    [BsonIgnoreExtraElements]
    public class RequirementObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int RequirementId { get; set; }
        public string UserId { get; set; }
        public int EnterpriseId { get; set; }

        
        public RequirementType RequirementTypeId { get; set; }

        /// <summary>
        /// 无用了
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 无用了
        /// </summary>
        public int ProductQuota { get; set; }

        public int ProductPrice { get; set; }


        /// <summary>
        /// 货物名称，
        /// </summary>
        public string ProductName { get; set; }


        /// <summary>
        /// 货物类型：有色、化工等
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 货物规格：Cu_Ag>=99.95%
        /// </summary>
        public string ProductSpecification { get; set; }

        /// <summary>
        /// 货物数量
        /// </summary>
        [BsonRepresentation(BsonType.Double)]
        public decimal ProductQuantity { get; set; }

        /// <summary>
        /// 获取单位， 吨
        /// </summary>
        public string ProductUnit { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// 货款支付时间
        /// </summary>
        public string PaymentDateTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        // public string PaymentType { get; set; }

        #region 仓储信息

        /// <summary>
        /// 仓库省份/区 如：上海、北京、浙江、江苏
        /// </summary>
        public string WarehouseState { get; set; }

        /// <summary>
        /// 仓库 城市，如：上海、北京、杭州、无锡
        /// </summary>
        public string WarehouseCity { get; set; }

        /// <summary>
        /// 详细地址1
        /// </summary>
        public string WarehouseAddress1 { get; set; }

        /// <summary>
        /// 详细地址2
        /// </summary>
        public string WarehouseAddress2 { get; set; }

        #endregion


        /// <summary>
        /// 出资和补贴两种需求都使用这个字段.
        /// 
        /// 出资方：出资总金额，我要购买xxx货物xxx吨，出资1亿
        /// 补贴方：贸易量，我要多少贸易量（1个亿的贸易量）
        /// </summary>
        [BsonRepresentation(BsonType.Double)]
        public decimal TradeAmount { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public decimal TradeProfit { get; set; }

        /// <summary>
        /// 企业类型
        /// </summary>
        public string EnterpriseType { get; set; }

        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessRange { get; set; }


        /// <summary>
        /// 补贴额度，比如：我要1个亿的贸易量，我补贴贸易量的5%
        /// </summary>

        [BsonRepresentation(BsonType.Double)]
        public decimal Subsidies { get; set; }

        /// <summary>
        /// 仓库开户
        /// </summary>
        public string WarehouseAccount { get; set; }

        /// <summary>
        /// 发票面额
        /// </summary>
        //public string InvoiceValue { get; set; }

        /// <summary>
        /// 发票开具时间
        /// </summary>
        public string InvoiceIssueDateTime { get; set; }

        /// <summary>
        /// 发票交接方式
        /// </summary>
        public string InvoiceTransferMode { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifyTime { get; set; } = DateTime.Now;
        public RequirementStatus RequirementStateId { get; set; }
        public bool Deleted { get; set; } = false;

        // TODO: add filter EnterpriceType

        public IList<RequirementFilter> Filters { get; set; }

        #region 1.1 新增属性

        /// <summary>
        /// 销售 - 交易要求
        /// </summary>
        public int TradeRequirement { get; set; }


        /// <summary>
        /// 支付方式 改枚举
        /// </summary>
        public int PaymentType { get; set; }


        /// <summary>
        /// 发票面额 改枚举
        /// </summary>
        public int InvoiceValue { get; set; }

        /// <summary>
        /// 电子仓单 - new
        /// </summary>
        public string EWarehouseReceipt { get; set; }

        /// <summary>
        /// 支付银行开户行名称 - new
        /// </summary>
        public string PaymentBankName { get; set; }

        /// <summary>
        /// 支付银行帐号 - new
        /// </summary>
        public string PaymentBankAccount { get; set; }

        /// <summary>
        /// 支付银行行号 - new
        /// </summary>
        public int PaymentBankId { get; set; }

        /// <summary>
        /// 支付银行地址 - new
        /// </summary>
        public string PaymentBankAddress { get; set; }

        /// <summary>
        /// 支付银行是否开通银承电子票口  - new
        /// </summary>
        public bool IsAcceptanceBillETicket { get; set; }

        /// <summary>
        /// 支付风控 - new
        /// </summary>
        public int PaymentRiskControl { get; set; }


        /// <summary>
        /// 开票要求 - new
        /// </summary>
        public int InvoiceRequirement { get; set; }

        /// <summary>
        /// 开票量（交易当月） - new
        /// </summary>
        public string InvoiceAmount { get; set; }

        /// <summary>
        /// 货物交接方式 - new
        /// </summary>
        public int ProductTransferMode { get; set; }

        /// <summary>
        /// 创建人UID
        /// </summary>
        public string OpUserId { get; set; }

        #endregion
    }

    /// <summary>
    /// RequirementFilter
    /// 方向从上至下为从买家向卖家
    /// 方向设置：
    /// FilterDirectionTypeId = DOWN 为买家对卖家的FILTER
    /// FilterDirectionTypeId = UP 为卖家对买家的FILTER
    /// </summary>
    public class RequirementFilter
    {
        public string FilterKey { get; set; }
        public FilterOperationType OperationTypeId { get; set; }
        public string FilterValue { get; set; }
        public FilterValueType FilterValueTypeId { get; set; }
        public bool IsSoftFilter { get; set; } = false;
        public FilterDirectionType FilterDirectionTypeId { get; set; } = 0;
    }

    public enum FilterDirectionType
    {
        BIDIRECT = 0,
        UP = 1,
        DOWN = 2
    }

    public enum FilterOperationType
    {
        EQ = 1,
        NE = 2,
        LE = 3,
        LT =4,
        GT = 5,
        GE = 6,
        IN = 7,
        NIN = 8
    }

    public enum FilterValueType
    {
        STRING = 1,
        NUMBER = 2,
        SEQUENCE_STRING = 3    }

    public enum RequirementType
    {
        BUYER = 1,
        SELLER = 2,
        MID = 3
    }

    public enum RequirementStatus
    {
        OPEN = 0,
        LOCKED = 1,
        CONFIRMED = 2
    }


}
