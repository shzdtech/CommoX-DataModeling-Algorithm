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
        Trade submitTrade(Trade trade);

        ///// <summary>
        ///// 提交交易链信息
        ///// </summary>
        ///// <param name="tradechain"></param>
        ///// <returns></returns>
        //TradeChain submitTradeChain(TradeChain tradechain);

        /// <summary>
        /// 查询一次交易情况
        /// </summary>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        Trade queryTrade(int tradeId);
        /// <summary>
        /// 查询用户下所有的交易情况
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<Trade> queryAllTrade(string userId);

        /// <summary>
        /// 查询交易链
        /// </summary>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        IList<TradeChain> queryTradeChain(int tradeId);
        /// <summary>
        /// 更新交易当前的状态：如到什么阶段：出资、出货等
        /// 数据来自requirementType
        /// </summary>
        /// <param name="tradeId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool updateTradeState(int tradeId, String state);

    }



    
}
