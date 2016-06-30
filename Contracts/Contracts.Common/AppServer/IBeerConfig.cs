using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Common.AppServer
{
    /// <summary>
    /// Интерфейс службы настройки справочников
    /// </summary>
    [ServiceContract]
    public interface IBeerConfig
    {
        /// <summary>
        /// Получить справочник сортов
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <returns>Список сортов</returns>
        [OperationContract]
        List<DrinkConfigDto> GetDrinksConfig(UserPass user);

        /// <summary>
        /// Добавить сорт
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="drinkConfig">Транспортный объект сорта</param>
        [OperationContract]
        void AddDrinkConfig(UserPass user, DrinkConfigDto drinkConfig);


        /// <summary>
        /// Править сорт
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="drinkConfig">Транспортный объект сорта</param>
        [OperationContract]
        void EditDrinkConfig(UserPass user, DrinkConfigDto drinkConfig);

        /// <summary>
        /// Удалить сорт
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="drinkId">Идентификатор сорта</param>
        [OperationContract]
        void DeleteDrinkConfig(UserPass user, int drinkId);

        /// <summary>
        /// Удалить поставщика
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="producerId">Идентификатор поставщика</param>
        [OperationContract]
        void DeleteProducerConfig(UserPass user, int producerId);

        /// <summary>
        /// Получить список цен для сорта
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="idDrink">Идентификатор сорта</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<MarketDto, PriceDto> GetDrinkPrices(UserPass user, int idDrink);

        /// <summary>
        /// Установить цены для магазинов
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="idDrink">Идентификатор сорта</param>
        /// <param name="marketPrices">Ключ - Идентификатор магазина </param>
        [OperationContract]
        void SetDrinkPrices(UserPass user, int idDrink, Dictionary<int, PriceDto> marketPrices);

        /// <summary>
        /// Получить список поставщиков
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <returns></returns>
        [OperationContract]
        List<ProducerDto> GetProducers(UserPass user);

        /// <summary>
        /// Добавить нового поставщика
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="producer">Поставщик</param>
        [OperationContract]
        void AddProducer(UserPass user, ProducerDto producer);

        /// <summary>
        /// Изменить параметры поставщика
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="producer">Поставщик</param>
        [OperationContract]
        void EditProducer(UserPass user, ProducerDto producer);

        /// <summary>
        /// Получить справочник магазинов
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <returns>Список магазинов</returns>
        [OperationContract]
        List<MarketConfigDto> GetMarketsConfig(UserPass user);

        /// <summary>
        /// Добавить новый магазин
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="marketConfig">Настройки магазина</param>
        [OperationContract]
        void AddMarketConfig(UserPass user, MarketDetailConfigDto marketConfig);

        /// <summary>
        /// Получить настройки одного магазина
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="idMarket">Идентификатор магазина</param>
        /// <returns>Настройки магазина</returns>
        [OperationContract]
        MarketDetailConfigDto GetMarketDetailConfig(UserPass user, int idMarket);

        /// <summary>
        /// Изменить настройки магазина
        /// </summary>
        /// <param name="user">Идентификатор пользователя</param>
        /// <param name="idMarket">Идентификатор магазина</param>
        /// <param name="marketConfig">Настройки магазина</param>
        [OperationContract]
        void SetMarketDetailConfig(UserPass user, int idMarket, MarketDetailConfigDto marketConfig);
    }
}
