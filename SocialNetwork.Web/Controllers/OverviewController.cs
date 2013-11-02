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
        public delegate IEnumerable<User> Search(IEnumerable<User> users, SearchModel model);

        public IEnumerable<User> SrchTerm(IEnumerable<User> users, SearchModel model)
        {
            var term = model.Term;
            if (string.IsNullOrWhiteSpace(term)) return users;

            var words = term.Split(' ');
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
            return users;
        }

        public IEnumerable<User> SrchCurrCity(IEnumerable<User> users, SearchModel model)
        {
            var city = model.CurrCity;
            if (string.IsNullOrWhiteSpace(city)) return users;
            users =
                users.Where(
                    item => String.Compare(item.CurrentCity, city.Trim(), StringComparison.OrdinalIgnoreCase) == 0);
            return users;
        }

        public IEnumerable<User> SrchSkype(IEnumerable<User> users, SearchModel model)
        {
            var skype = model.Skype;
            if (string.IsNullOrWhiteSpace(skype)) return users;

            users = users.Where(item => String.Compare(item.Skype, skype, StringComparison.OrdinalIgnoreCase) == 0);
            return users;
        }

        public IEnumerable<User> SrchEmail(IEnumerable<User> users, SearchModel model)
        {
            var email = model.Email;
            if (string.IsNullOrWhiteSpace(email)) return users;

            users = users.Where(item => String.Compare(item.Email, email, StringComparison.OrdinalIgnoreCase) == 0);
            return users;
        }

        public IEnumerable<User> SrchSex(IEnumerable<User> users, SearchModel model)
        {
            var sex = model.Sex;
            if (sex == null) return users;

            users = users.Where(item => item.Sex == (bool) sex);
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
            var users = UserRepository.GetAll();

            var searchModel = new SearchModel
                {
                    Email = email,
                    Sex = sex,
                    Skype = skype,
                    CurrCity = currentcity,
                    Term = srchterm
                };

            Search search = null;
            if (!string.IsNullOrWhiteSpace(srchterm))
                search += SrchTerm;
            if (!string.IsNullOrWhiteSpace(currentcity))
                search += SrchCurrCity;
            if (!string.IsNullOrWhiteSpace(skype))
                search += SrchSkype;
            if (!string.IsNullOrWhiteSpace(email))
                search += SrchEmail;
            if (sex!=null)
                search += SrchSex;

            if (search != null) users = search(users, searchModel);

            var usersViewModels = users.Select(
                item => (UserViewModel) mapper.Map(item, typeof (User), typeof (UserViewModel)))
                .ToPagedList(currentPageIndex, DefaultPageSize);

            return View(usersViewModels);
        }
    }
}