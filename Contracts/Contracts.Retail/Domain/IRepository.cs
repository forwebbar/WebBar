using System;
using System.Collections.Generic;
using Contracts.Common;

namespace Contracts.Retail.Domain
{
    /// <summary>
    /// ��������� �������� ��������
    /// </summary>
    public interface IRepository : IDisposable
    {
        List<Store> GetStores(UserPass pass);
    }
}