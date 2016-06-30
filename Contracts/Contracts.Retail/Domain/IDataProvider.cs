using System.Collections.Generic;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// ��������� ������ ������
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// �������� ������ �� ���������
        /// </summary>
        /// <param name="stores"></param>
        void Update(List<Store> stores);
    }
}