using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Contracts.Common.AppServer
{
    [ServiceContract]
    public interface IBeerData
    {
        /// <summary>
        /// Получить список магазинов пользователя
        /// </summary>
        /// <param name="user">Пропуск пользователя</param>
        /// <returns>Список магазинов</returns>
        [OperationContract]
        List<MarketDto> GetMarkets(UserPass user);

        /// <summary>
        /// Получить список напитков пользователя
        /// </summary>
        /// <param name="user">Пропуск пользователя</param>
        /// <returns>Список напитков</returns>
        [OperationContract]
        List<DrinkDto> GetDrinks(UserPass user);

        /// <summary>
        /// Получить сводную информацию по продажам для всех магазинов
        /// </summary>
        /// <param name="user">Пропуск пользователя</param>
        /// <param name="startTs">Срок "С"</param>
        /// <param name="endTs">Срок "По"</param>
        /// <returns>Транспортный объект суммарных продаж с привязкой к маагазину</returns>
        [OperationContract]
        Dictionary<MarketDto, SellSummaryDto> GetSellSummary(UserPass user, DateTimeOffset startTs, DateTimeOffset endTs);

        /// <summary>
        /// Получить сводную информацию по продажам для магазина
        /// </summary>
        /// <param name="user">Пропуск пользователя</param>
        /// <param name="marketId">Идентификатор магазина</param>        
        /// <param name="startTs">Срок "С"</param>
        /// <param name="endTs">Срок "По"</param>
        /// <returns>Словарь напиток - Транспортный объект суммарных продаж</returns>
        [OperationContract]
        Dictionary<DrinkDto, SellSummaryDto> GetMarketSummary(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs);

        /// <summary>
        /// Получить список продаж по магазину
        /// </summary>
        /// <param name="user">Пропуск пользователя</param>
        /// <param name="marketId">Идентификатор магазина</param>
        /// <param name="startTs">Срок "С"</param>
        /// <param name="endTs">Срок "По"</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<DrinkDto, List<SellDto>> GetMarketSells(UserPass user, int marketId, DateTimeOffset startTs, DateTimeOffset endTs);

        /// <summary>
        /// Получить список продаж по напитку
        /// </summary>
        /// <param name="user">Пропуск пользователя</param>
        /// <param name="drinkId">Идентификатор напитка</param>
        /// <param name="startTs">Срок "С"</param>
        /// <param name="endTs">Срок "По"</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<MarketDto, List<SellDto>> GetDrinkSells(UserPass user, int drinkId, DateTimeOffset startTs, DateTimeOffset endTs);

        [OperationContract]
        void SetSellStatus(UserPass user, SellDto sell, bool isCleaning);
    }
}
