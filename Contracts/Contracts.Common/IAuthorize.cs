using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Common
{
    /// <summary>
    /// Интерфейс службы авторизации
    /// </summary>
    [ServiceContract]
    public interface IAuthorize
    {
        /// <summary>
        /// Войти в систему
        /// </summary>
        /// <param name="login">Имя пользователя</param>
        /// <param name="password">Хеш - пароля</param>
        /// <returns>Токен подтверждения операции. 
        /// Если неверно указан логин или пароль возвращается ошибка UserNotFoundFault.
        /// Если пользователь существует, но временно заблокирован (например, по причине ввода неверного пароля несколько раз подряд) 
        /// возвращается ошибка UserBlockedFault
        /// </returns>
        [OperationContract]
        [FaultContract(typeof (UserNotFoundFault))]
        [FaultContract(typeof (UserBlockedFault))]
        ConfirmToken Login(string login, byte[] password);

        /// <summary>
        /// Подтвердить предыдущую операцию
        /// </summary>
        /// <param name="token">Токен подтверждения операции</param>
        /// <param name="confirmCode">Код подтверждения операции</param>
        /// <returns>Пропуск пользователя.
        /// Если введен неверный код подтверждения или несуществующий токен возвращается ошибка InvalidConfirmFault.
        /// Если токен и код верны, но прошел таймаут подтверждения возвращается ошибка ConfirmTokenExpiredFault</returns>
        [OperationContract]
        [FaultContract(typeof(InvalidConfirmFault))]
        [FaultContract(typeof(ConfirmTokenExpiredFault))]
        UserPass ConfirmOperation(ConfirmToken token, byte[] confirmCode);

        /// <summary>
        /// Установить новый пароль пользователя
        /// </summary>
        /// <param name="pass">Пропуск пользователя</param>
        /// <param name="oldPassword">Старый пароль</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns>Токен подтверждения операции.
        /// Если неверно указан старый пароль возвращается ошибка UserNotFoundFault.
        /// Если просрочен пропуск пользователя возвращается ошибка UserPassExpiredFault
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(UserNotFoundFault))]
        [FaultContract(typeof(UserPassExpiredFault))]
        ConfirmToken SetPassword(UserPass pass, string oldPassword, string newPassword);

        /// <summary>
        /// Получить подробные данные пользователя
        /// </summary>
        /// <param name="pass">Пропуск пользователя</param>
        /// <returns>Подробные данные пользователя.
        /// Если просрочен пропуск пользователя возвращается ошибка UserPassExpiredFault
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(UserPassExpiredFault))]
        UserDetails GetUserDetails(UserPass pass);

        /// <summary>
        /// Записать подробные данные пользователя
        /// </summary>
        /// <param name="pass">Пропуск пользователя</param>
        /// <param name="newDetails">Новые подробные данные пользователя</param>
        /// <returns> Токен подтверждения операции.
        /// Если просрочен пропуск пользователя возвращается ошибка UserPassExpiredFault
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(UserPassExpiredFault))]
        ConfirmToken SetUserDetails(UserPass pass, UserDetails newDetails);

        /// <summary>
        /// Продлить просроченный пропуск пользователя
        /// </summary>
        /// <param name="pass">Просроченный пропуск пользователя</param>
        /// <returns>Новый пропуск пользователя.
        /// Если более невозможно продлить пропуск возвращается ошибка UserPassExpiredFault</returns>
        [OperationContract]        
        [FaultContract(typeof (UserPassExpiredFault))]
        UserPass ProlongUserPass(UserPass pass);

        /// <summary>
        /// Восстановить пароль пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// В случае если пользователь не найден возвращается ошибка UserNotFoundFault.
        /// В случае если пользователь временно заблокирован возвращается ошибка UserBlockedFault.
        [OperationContract]
        [FaultContract(typeof (UserNotFoundFault))]
        [FaultContract(typeof(UserBlockedFault))]
        void RestorePassword(string login);
    }
}
