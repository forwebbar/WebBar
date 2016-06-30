using System.Collections.Generic;
using Contracts.Common;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// ��������� ���� ����������� �������� ��������
    /// </summary>
    public interface IRepositoryCache
    {
        /// <summary>
        /// ��������� ������ ��������� ��� ������������
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        List<Store> GetItems(UserPass pass);
    }
}