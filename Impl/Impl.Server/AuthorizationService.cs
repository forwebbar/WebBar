using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Logging;
using Contracts.Common;

namespace Impl.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuthorizationService : IAuthorize
    {
        #region Fields

        private readonly ILog _logger = LogManager.GetLogger<AuthorizationService>();

        #endregion


        #region Properties

        public IEnumerable<UserDetails> FakeUsers = new List<UserDetails>
        {
            new UserDetails
            {
                 Username = "1",
                 Password = "11",
                 Email = "north@live.ru",
                 Code = Encoding.UTF8.GetBytes("111")
            }
        };

        #endregion

        #region Methods

        public ConfirmToken Login(string login, byte[] password)
        {
            if (password == null)
                return null;

            var textPassword = Encoding.UTF8.GetString(password);

            var user = FakeUsers.FirstOrDefault(a => a.Username == login && a.Password == textPassword);
            if (user == null)
                return null;

            return new ConfirmToken { Username = login };
        }

        public UserPass ConfirmOperation(ConfirmToken token, byte[] confirmCode)
        {
            var user = FakeUsers.FirstOrDefault(a => a.Username == token.Username);
            if (user == null)
                return null;

            if (token == null || confirmCode == null || Encoding.UTF8.GetString(confirmCode) != Encoding.UTF8.GetString(user.Code))
                return null;

            return new UserPass {Email = user.Email, Username = user.Username};
        }

        public ConfirmToken SetPassword(UserPass pass, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public UserDetails GetUserDetails(UserPass pass)
        {
            return FakeUsers.FirstOrDefault(a => a.Username == pass.Username);
        }

        public ConfirmToken SetUserDetails(UserPass pass, UserDetails newDetails)
        {
            throw new NotImplementedException();
        }

        public UserPass ProlongUserPass(UserPass pass)
        {
            throw new NotImplementedException();
        }

        public void RestorePassword(string login)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
