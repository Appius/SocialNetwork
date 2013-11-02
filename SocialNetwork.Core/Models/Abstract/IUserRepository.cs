#region

using System;
using System.Collections.Generic;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IUserRepository
    {
        /// <summary>
        ///     Получение пользователя по его уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Пользователь</returns>
        User Get(int id);

        /// <summary>
        ///     Получение пользователя по его email
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>Пользователь</returns>
        User Get(string email);

        /// <summary>
        ///     Получение списка всех пользователей
        /// </summary>
        /// <returns>Коллекция пользователей</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        ///     Добавить пользователя в базу данных
        /// </summary>
        /// <param name="user">Пользователь</param>
        void Add(User user);

        /// <summary>
        ///     Обновить данные пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        void Update(User user);

        /// <summary>
        ///     Удаление пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        void Delete(int id);

        /// <summary>
        ///     Удаление пользователя
        /// </summary>
        /// <param name="user">Пользоватепь</param>
        void Delete(User user);

        /// <summary>
        ///     Авторизация пользователя
        /// </summary>
        /// <param name="email">E-nail</param>
        /// <param name="password">Пароль</param>
        /// <returns>Пользователь</returns>
        User Login(string email, string password);

        /// <summary>
        ///     Заблокировать/разблокировать пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        void Block(int id);
    }
}