
using NUnit.Framework;
using WebBar.Site.Factories;

namespace WebBar.Site.UnitTests.Security
{

    [TestFixture]
    public class AccountTest
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TransferFunds()
        {

            var fct = new AuthorizeChannelFactory();
            var ch = fct.CreateChannel();



            //Assert.AreEqual(100m, source.Balance);
        }
        
    }
}
