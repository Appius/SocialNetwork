#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IUserRoleRepository
    {
        /// <summary>
        ///     Добавление роли к пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        void Add(User user, Role role);


        /// <summary>
        ///     Удаление роли у пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        void Delete(User user, Role role);

        /// <summary>
        ///     Имеет ли пользователь заданую роль
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        bool Contains(User user, Role role);
    }
}