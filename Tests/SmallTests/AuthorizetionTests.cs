using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Impl.Client;
using Contracts.Common;
using System.ServiceModel;

namespace SmallTests
{
    [TestClass]
    public class AuthorizetionTests
    {
        /// <summary>
        /// Тест показывает удачный вход на сайт
        /// </summary>
        [TestMethod]
        public void Login_Success()
        {
            //Запросили логин и пароль, юзер их ввел
            var login = "1";
            var password = new byte[] { 0x1, 0x1 };

            UserPass userPass;
            using (var client = new StubAuthorizeService())
            {
                //Обращаемся на сервер чтобы авторизоваться
                ConfirmToken cnfToken = client.Login(login, password);
                //Не было исключений и мы получли токен подтверждения

                //Запросим у юзера код подтверждения, служба авторизации сама отправляет юзеру код
                byte[] confirmCode = { 0x98, 0x77, 0x11 };

                //Отдаем код подтверждения введенный юзером
                userPass = client.ConfirmOperation(cnfToken, confirmCode);
                //Все Ок, мы получили UserPass и можем с ним обращаться к службе данных
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Login_UserBlocked_Error()
        {
            //Запросили логин и пароль, юзер их ввел
            var login = "1";
            var password = new byte[] { 0x1, 0x1 };

            using (var client = new StubAuthorizeService())
            {
                //Пользователь заблокирован
                client.BlockUser();
                try
                {
                    ConfirmToken cnfToken = client.Login(login, password);
                    Assert.IsTrue(false);
                }
                catch (FaultException<UserBlockedFault> ex)
                {
                    //Выводим сообщение, что учетная запись заблокирована
                    Assert.IsTrue(true);
                }
            }
        }


        [TestMethod]
        public void Login_ToLongTimeout_Error()
        {
            //Запросили логин и пароль, юзер их ввел
            var login = "1";
            var password = new byte[] { 0x1, 0x1 };

            UserPass userPass;
            using (var client = new StubAuthorizeService())
            {
                //Обращаемся на сервер чтобы авторизоваться
                ConfirmToken cnfToken = client.Login(login, password);
                //Не было исключений и мы получли токен подтверждения

                //Запросим у юзера код подтверждения
                byte[] confirmCode = { 0x98, 0x77, 0x11 };

                //Юзер очень долго тупил и долго не вводил код подтверждения, но таки ввел
                client.ExpireToken();

                try {
                    //Отдаем код подтверждения введенный юзером
                    userPass = client.ConfirmOperation(cnfToken, confirmCode);
                    Assert.IsFalse(true);
                }
                catch(FaultException<ConfirmTokenExpiredFault> ex)
                {
                    //Выводим сообщение, что истекло время ожидания для кода подтверждения, 
                    //необходимо просвести авторизацию заново
                    Assert.IsTrue(true);
                }
            }
        }

        [TestMethod]
        public void ProlongUserPass_Success()
        {
            //Мы удачно авторизовались ранее и получили userPass, но прошло много времени и пропуск просрочен
            UserPass userPass = new ExpiredUserPass();

            //Используем его для работы со службой данных, или как в примере ниже, смотрим свойства юзера
            //но получаем исключение UserPassExpiredFault
            using (var client = new StubAuthorizeService())
            {
                try
                {
                    var details = client.GetUserDetails(userPass);
                    Assert.IsFalse(true);
                }
                catch(FaultException<UserPassExpiredFault> ex)
                {
                    //Пробуем продлить userPass
                    userPass = client.ProlongUserPass(userPass);
                    //Получили новый userPass, можем выполнить нужный нам запрос
                    var details = client.GetUserDetails(userPass);
                    //Теперь все удачно
                    Assert.IsTrue(true);
                }
            }
        }

        internal class StubAuthorizeService : IAuthorize, IDisposable
        {
            bool _userBlocked = false;
            bool _tokenExpired = false;

            public ConfirmToken Login(string login, byte[] password)
            {
                if ((login != "1") || (password == null))
                    throw new FaultException<UserNotFoundFault>(new UserNotFoundFault());

                if (_userBlocked == true)
                    throw new FaultException<UserBlockedFault>(new UserBlockedFault());

                return new ConfirmToken();
            }

            public UserPass ConfirmOperation(ConfirmToken token, byte[] confirmCode)
            {
                if (token == null || confirmCode == null)
                    throw new FaultException<InvalidConfirmFault>(new InvalidConfirmFault());

                if (_tokenExpired)
                    throw new FaultException<ConfirmTokenExpiredFault>(new ConfirmTokenExpiredFault());

                return new UserPass();
            }


            public ConfirmToken SetPassword(UserPass pass, string oldPassword, string newPassword)
            {
                throw new NotImplementedException();
            }

            public UserDetails GetUserDetails(UserPass pass)
            {
                if(pass is ExpiredUserPass)
                    throw new FaultException<UserPassExpiredFault>(new UserPassExpiredFault());

                return new UserDetails();
            }

            public ConfirmToken SetUserDetails(UserPass pass, UserDetails newDetails)
            {
                throw new NotImplementedException();
            }

            public UserPass ProlongUserPass(UserPass pass)
            {
                return new UserPass();
            }

            public void RestorePassword(string login)
            {
                throw new NotImplementedException();
            }

            internal void BlockUser()
            {
                _userBlocked = true;
            }

            internal void ExpireToken()
            {
                _tokenExpired = true;
            }

            #region IDisposable Support
            private bool disposedValue = false; // Для определения избыточных вызовов

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: освободить управляемое состояние (управляемые объекты).
                    }

                    // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                    // TODO: задать большим полям значение NULL.

                    disposedValue = true;
                }
            }

            // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
            public void Dispose()
            {
                // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
                Dispose(true);
                // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
                // GC.SuppressFinalize(this);
            }
            #endregion
        }
    }

    internal class ExpiredUserPass : UserPass
    {

    }
}
        