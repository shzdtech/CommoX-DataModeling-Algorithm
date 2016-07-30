using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface ITrade
    {

        /// <summary>
        /// 提交一次交易信息
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        Boolean submitTrade(Trade trade);

        /// <summary>
        /// 提交交易链信息
        /// </summary>
        /// <param name="tradechain"></param>
        /// <returns></returns>
        Boolean submitTradeChain(TradeChain tradechain);

        /// <summary>
        /// 查询一次交易情况
        /// </summary>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        Trade queryTrade(int tradeId);
        /// <summary>
        /// 查询交易链
        /// </summary>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        IEnumerable<TradeChain> queryTradeChain(int tradeId);

        }
    
}
