#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Core.Models.Repos
{
    /// <summary>
    ///     Класс репозитория по работе с пользователями
    /// </summary>
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(SocialNetworkContext db)
            : base(db)
        {
        }

        /// <summary>
        ///     Получение пользователя по его уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Пользователь</returns>
        public User Get(int id)
        {
            return Db.Users.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        ///     Получение пользователя по его email
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>Пользователь</returns>
        public User Get(string email)
        {
            return Db.Users.FirstOrDefault(item => item.Email == email);
        }

        /// <summary>
        ///     Получение списка всех пользователей
        /// </summary>
        /// <returns>Коллекция пользователей</returns>
        public IEnumerable<User> GetAll()
        {
            return Db.Users;
        }

        /// <summary>
        ///     Добавить пользователя в базу данных
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void Add(User user)
        {
            Db.Users.Add(user);
            Db.SaveChanges();
        }

        /// <summary>
        ///     Обновить данные пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void Update(User user)
        {
            Db.Entry(user).State = EntityState.Modified;
            Db.SaveChanges();
        }

        /// <summary>
        ///     Удаление пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public void Delete(int id)
        {
            Delete(Get(id));
        }

        /// <summary>
        ///     Удаление пользователя
        /// </summary>
        /// <param name="user">Пользоватепь</param>
        public void Delete(User user)
        {
            Db.Users.Remove(user);
            Db.SaveChanges();
        }

        /// <summary>
        ///     Авторизация пользователя
        /// </summary>
        /// <param name="email">E-nail</param>
        /// <param name="password">Пароль</param>
        /// <returns>Пользователь</returns>
        public User Login(string email, string password)
        {
            return Db.Users.FirstOrDefault(item => item.Email == email && item.Password == password);
        }
    }
}