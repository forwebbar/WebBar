using System.Collections.Generic;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// Провайдер службы данных
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Обновить данные по магазинам
        /// </summary>
        /// <param name="stores"></param>
        void Update(List<Store> stores);
    }
}