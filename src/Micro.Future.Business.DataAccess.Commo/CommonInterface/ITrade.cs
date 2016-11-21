﻿using Micro.Future.Business.DataAccess.Commo.CommoObject;
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

        IList<Trade> queryAllTrade(string tradeState);


        IList<Trade> queryTradesByEnterprise(int enterpriseId, string tradeState);

        /// <summary>
        /// 更新交易当前的状态
        /// </summary>
        /// <param name="tradeId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool updateTradeState(int tradeId, string state);
    }



    
}
