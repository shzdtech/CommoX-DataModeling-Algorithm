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
        /// 指定的一些需求id列表，不能为空，或者全是0。
        /// 固定位置时，留空的需求Id填0.
        /// </param>
        /// <param name="fixedLength">传值表示固定长度</param>
        /// <param name="isPositionFixed">已有的需求是否固定位置。 true表示固定位置；false表示不固定 </param>
        /// <returns></returns>
        ChainObject AutoMatchRequirements(string opUserId, IList<int> requirementIds, int fixedLength, bool isPositionFixed, int maxLength);
    }
}
