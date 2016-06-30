using System.Collections.Generic;
using Contracts.Common;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Интерфейс кеша репозитория доменных объектов
    /// </summary>
    public interface IRepositoryCache
    {
        /// <summary>
        /// Вовращает список магазинов для пользователя
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        List<Store> GetItems(UserPass pass);
    }
}