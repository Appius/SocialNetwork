#region

using System;
using System.Linq;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Core.Models.Repos
{
    public class UserRoleRepository : RepositoryBase, IUserRoleRepository
    {
        /// <summary>
        ///     Добавление роли к пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        public void Add(User user, Role role)
        {
            Db.UserRoles.Add(new UserRole {RoleId = role.Id, UserId = user.Id});
            Db.SaveChanges();
        }

        /// <summary>
        ///     Удаление роли у пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        public void Delete(User user, Role role)
        {
            var userRole = Db.UserRoles.FirstOrDefault(item => item.User == user && item.Role == role);
            if (userRole != null)
            {
                Db.UserRoles.Remove(userRole);
                Db.SaveChanges();
            }
        }

        /// <summary>
        ///     Имеет ли пользователь заданную роль
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        public bool Contains(User user, Role role)
        {
            return Db.UserRoles.FirstOrDefault(item => item.UserId == user.Id && item.RoleId == role.Id) != null;
        }
    }
}