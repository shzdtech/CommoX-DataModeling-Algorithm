using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo.BizObjects;

namespace Micro.Future.Business.MatchMaker.Abstraction.Models
{
    public interface BaseMatchMaker
    {
        void make();
        IList<RequirementObject> FindReplacedRequirementsForChain(int chainId, int replacedNodeIndex, int topN);

        /// <summary>
        /// 指定几个需求，撮合一条需求列表
        /// </summary>
        /// <param name="requirementIds">
        /// 指定的一些需求id列表，不能为空，或者全是0。 -1表示一个占位符， 0表示0个或者多个占位符， 买家和卖家必须至少给出一家， 未给出的买家或者卖家必须由-1占位符代替
        /// 例如： 
        /// 固定位置时： -1, 0, midId1, midId2, sellerId 或者 buyerId, midId, 0, -1
        /// 未固定位置时也需要指定买家或者卖家的占位符(放在对应的头或者尾)，例如： -1, midId1, midId2, sellerId 或者 buyerid, midId, -1
        /// </param>
        /// <param name="fixedLength">传值表示固定长度</param>
        /// <param name="isPositionFixed">已有的需求是否固定位置。 true表示固定位置；false表示不固定 </param>
        /// <param name="maxLength">当长度不固定时， 给定的一个最大长度的限制， 若fixedLength > 0 ， 则该参数无效</param>
        /// <returns></returns>
        ChainObject AutoMatchRequirements(string opUserId, IList<int> requirementIds, int fixedLength = 0, bool isPositionFixed = false, int maxLength = 6);
    }
}
