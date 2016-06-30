using Contracts.Common;

namespace WebBar.BeerServer.Security
{
    internal interface ISecurityProvider
    {
        bool Check(UserPass user);
        int GetUserId(UserPass user);
    }
}