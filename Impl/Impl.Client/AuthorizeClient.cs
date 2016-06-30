using System.ServiceModel;
using Contracts.Common;

namespace Impl.Client
{
    public class AuthorizeClient : ClientBase<IAuthorize>, IAuthorize
    {
        public virtual ConfirmToken Login(string login, byte[] password)
        {
            return Channel.Login(login, password);
        }

        public virtual UserPass ConfirmOperation(ConfirmToken token, byte[] confirmCode)
        {
            return Channel.ConfirmOperation(token, confirmCode);
        }

        public virtual ConfirmToken SetPassword(UserPass pass, string oldPassword, string newPassword)
        {
            return Channel.SetPassword(pass, oldPassword, newPassword);
        }

        public virtual UserDetails GetUserDetails(UserPass pass)
        {
            return Channel.GetUserDetails(pass);
        }

        public virtual ConfirmToken SetUserDetails(UserPass pass, UserDetails newDetails)
        {
            return Channel.SetUserDetails(pass, newDetails);
        }

        public virtual UserPass ProlongUserPass(UserPass pass)
        {
            return Channel.ProlongUserPass(pass);
        }

        public virtual void RestorePassword(string login)
        {
            Channel.RestorePassword(login);
        }
    }
}
