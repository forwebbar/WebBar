using System;
using Contracts.Common;

namespace WebBar.Site.ServiceClients
{
    public class AuthorizeService : IAuthorize
    {
        #region Methods

        public AuthorizeService(AuthorizeWcfServiceClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _client = client;
        }

        #endregion

        #region Fields

        private readonly AuthorizeWcfServiceClient _client;

        #endregion

        #region Methods

        public ConfirmToken Login(string login, byte[] password)
        {
            var res = _client.Execute(a => a.Login(login, password));
            return res;
        }

        public UserPass ConfirmOperation(ConfirmToken token, byte[] confirmCode)
        {
            var res = _client.Execute(a => a.ConfirmOperation(token, confirmCode));
            return res;
        }

        public ConfirmToken SetPassword(UserPass pass, string oldPassword, string newPassword)
        {
            var res = _client.Execute(a => a.SetPassword(pass, oldPassword, newPassword));
            return res;
        }

        public UserDetails GetUserDetails(UserPass pass)
        {
            var res = _client.Execute(a => a.GetUserDetails(pass));
            return res;
        }

        public ConfirmToken SetUserDetails(UserPass pass, UserDetails newDetails)
        {
            var res = _client.Execute(a => a.SetUserDetails(pass, newDetails));
            return res;
        }

        public UserPass ProlongUserPass(UserPass pass)
        {
            var res = _client.Execute(a => a.ProlongUserPass(pass));
            return res;
        }

        public void RestorePassword(string login)
        {
            _client.Execute(a => a.RestorePassword(login));
        }

        #endregion
    }
}