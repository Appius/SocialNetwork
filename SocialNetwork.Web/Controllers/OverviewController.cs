#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MvcPaging;
using SocialNetwork.Core.Helpers;
using SocialNetwork.Core.Models;
using SocialNetwork.Web.Mappers;
using SocialNetwork.Web.ViewModels;
using WebGrease.Css.Extensions;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class OverviewController : BaseController
    {
        /// <summary>
        ///     Поиск пользователя
        /// </summary>
        /// <param name="users">Коллекция пользователей</param>
        /// <param name="srchTerm">Слово для поиска</param>
        /// <param name="currCity">Текущий город</param>
        /// <param name="skype">Skype</param>
        /// <param name="email">E-mail</param>
        /// <param name="sex">Пол</param>
        private static IEnumerable<User> Search(IEnumerable<User> users, string srchTerm, string currCity, string skype,
            string email, bool? sex = null)
        {
            #region Search term

            if (!string.IsNullOrWhiteSpace(srchTerm))
            {
                var words = srchTerm.Split(' ');
                switch (words.Length)
                {
                    case 2:
                    {
                        users = users.Where(item =>
                            (String.Compare(item.FirstName, words[0], StringComparison.OrdinalIgnoreCase) == 0 &&
                             String.Compare(item.LastName, words[1], StringComparison.OrdinalIgnoreCase) == 0)
                            || (String.Compare(item.LastName, words[0], StringComparison.OrdinalIgnoreCase) == 0 &&
                                String.Compare(item.FirstName, words[1], StringComparison.OrdinalIgnoreCase) == 0));
                        break;
                    }
                    default:
                    {
                        users = users.Where(item =>
                            String.Compare(item.FirstName, words[0].Trim(), StringComparison.OrdinalIgnoreCase) == 0
                            || String.Compare(item.LastName, words[0].Trim(), StringComparison.OrdinalIgnoreCase) == 0
                            || String.Compare(item.MiddleName, words[0].Trim(), StringComparison.OrdinalIgnoreCase) == 0);
                        break;
                    }
                }
            }

            #endregion

            #region Current City

            if (!string.IsNullOrWhiteSpace(currCity))
            {
                string city = currCity;
                users =
                    users.Where(
                        item => String.Compare(item.CurrentCity, city.Trim(), StringComparison.OrdinalIgnoreCase) == 0);
            }

            #endregion

            #region Skype

            {
                if (!string.IsNullOrWhiteSpace(skype))
                {
                    skype = skype.Trim();

                    users =
                        users.Where(
                            item => String.Compare(item.Skype, skype, StringComparison.OrdinalIgnoreCase) == 0);
                }
            }

            #endregion

            #region E-mail

            if (!string.IsNullOrWhiteSpace(email))
            {
                email = email.Trim();

                users =
                    users.Where(
                        item => String.Compare(item.Email, email, StringComparison.OrdinalIgnoreCase) == 0);
            }

            #endregion

            #region Sex

            if (sex != null)
            {
                users = users.Where(item => item.Sex == sex);
            }

            #endregion

            return users;
        }

        /// <summary>
        ///     Главная страница
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="srchterm">Слово для поиска</param>
        /// <param name="currentcity">Текущий город</param>
        /// <param name="skype">Skype</param>
        /// <param name="email">E-mail</param>
        /// <param name="sex">Пол</param>
        public ActionResult Index(int? page, string srchterm, string currentcity, string skype, string email,
            bool? sex)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            var mapper = DependencyResolver.Current.GetService<IMapper>();
            IEnumerable<User> users = UserRepository.GetAll();

            users = Search(users, srchterm, currentcity, skype, email, sex);

            var enumerableUsers = users as User[] ?? users.ToArray();

            var usersViewModels = enumerableUsers.Select(
                item => (UserViewModel) mapper.Map(item, typeof (User), typeof (UserViewModel)))
                .ToPagedList(currentPageIndex, DefaultPageSize);

            return View(usersViewModels);
        }
    }
}