using Contracts.Common;

namespace WebBar.BeerServer.Security
{
    internal class FakeSecurityProvider : ISecurityProvider
    {
        public bool Check(UserPass user)
        {
            return true;
        }

        public int GetUserId(UserPass user)
        {
            return 3;
        }
    }
}