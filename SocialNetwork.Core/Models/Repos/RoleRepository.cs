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
    ///     Класс работы с репозиторием ролей
    /// </summary>
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        /// <summary>
        ///     Получение роли по ее id
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns>Роль</returns>
        public Role Get(int id)
        {
            return Db.Roles.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        ///     Получение списка ролей
        /// </summary>
        /// <returns>Коллекция ролей</returns>
        public IEnumerable<Role> GetAll()
        {
            return Db.Roles;
        }

        /// <summary>
        ///     Добавление новой роли
        /// </summary>
        /// <param name="role">Роль</param>
        public void Add(Role role)
        {
            Db.Roles.Add(role);
            Db.SaveChanges();
        }

        /// <summary>
        ///     Обновление данных о роли
        /// </summary>
        /// <param name="role">Роль</param>
        public void Update(Role role)
        {
            Db.Entry(role).State = EntityState.Modified;
            Db.SaveChanges();
        }

        /// <summary>
        ///     Удаление роли
        /// </summary>
        /// <param name="role">Роль</param>
        public void Delete(Role role)
        {
            Db.Roles.Remove(role);
            Db.SaveChanges();
        }

        /// <summary>
        ///     Удаление роли
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        public void Delete(int id)
        {
            Delete(Get(id));
        }
    }
}