using System.Collections.Generic;
using Contracts.Retail.Domain;

namespace Retail.SmallTests.Stubs
{
    /// <summary>
    /// �������� ���������� ��������� ������
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MockDataProvider : IDataProvider
    {
        public void Update(List<Store> stores)
        {
            return;
        }
    }
}