#region

using System;
using System.Collections.Generic;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IRoleRepository
    {
        /// <summary>
        ///     Получение роли по ее id
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns>Роль</returns>
        Role Get(int id);

        /// <summary>
        ///     Получение роли по ее имени
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <returns>Роль</returns>
        Role Get(string name);

        /// <summary>
        ///     Получение списка ролей
        /// </summary>
        /// <returns>Коллекция ролей</returns>
        IEnumerable<Role> GetAll();

        /// <summary>
        ///     Добавление новой роли
        /// </summary>
        /// <param name="role">Роль</param>
        void Add(Role role);

        /// <summary>
        ///     Обновление данных о роли
        /// </summary>
        /// <param name="role">Роль</param>
        void Update(Role role);

        /// <summary>
        ///     Удаление роли
        /// </summary>
        /// <param name="role">Роль</param>
        void Delete(Role role);

        /// <summary>
        ///     Удаление роли
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        void Delete(int id);
    }
}