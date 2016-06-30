using System.Reflection;
using Contracts.Common;
using Contracts.Retail.Domain;
using Impl.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Retail.SmallTests.Stubs;

namespace Retail.SmallTests
{
    [TestClass]
    public class DomainModelTests
    {
        private StandardKernel _kernel;

        [TestInitialize]
        public void Init()
        {
            _kernel = new StandardKernel(new Bindings());
            _kernel.Load(Assembly.GetExecutingAssembly());
        }

        [TestMethod]
        public void GetStoreListForUser_Success()
        {
            var pass = new UserPass();            

            using (var repository = _kernel.Get<StoreRepository>())
            {
                var stores =  repository.GetStores(pass);
                Assert.IsTrue(stores.Count == 3);
            }
        }
    }
}
