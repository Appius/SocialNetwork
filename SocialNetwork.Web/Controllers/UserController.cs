#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MvcPaging;
using SocialNetwork.Core.Helpers;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Web.App_GlobalResources;
using SocialNetwork.Web.Mappers;
using SocialNetwork.Web.ViewModels;
using WebGrease.Css.Extensions;

#endregion

namespace SocialNetwork.Web.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        ///     Главная страница просмотра информации о пользователе
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя (0 - твоя страница)</param>
        [Authorize]
        public ActionResult Index(int? id)
        {
            if (id == null) id = CurrentUser.Id;
            var user = UserRepository.Get((int)id);

            if (user == null) return View();

            var mapper = DependencyResolver.Current.GetService<IMapper>();
            var userFullInfo = (UserFullInfoViewModel)mapper.Map(user, typeof(User), typeof(UserFullInfoViewModel));

            var friendshipRepository = DependencyResolver.Current.GetService<IFriendShipRepository>();
            userFullInfo.IsFriend = friendshipRepository.IsFriends(user, CurrentUser);

            return View(userFullInfo);
        }

        /// <summary>
        ///     Страница просмотра сообщений
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="act">Категория</param>
        [Authorize]
        public ActionResult Messages(int? page, string act = "")
        {
            var mapper = DependencyResolver.Current.GetService<IMapper>();
            var currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            var messageRepository = DependencyResolver.Current.GetService<IMessageRepository>();

            IEnumerable<Message> msgs;
            switch (act)
            {
                case "outbox":
                    msgs = messageRepository.GetOutbox(CurrentUser).OrderByDescending(item => item.PostedDate).ToList();
                    break;
                default:
                    msgs = messageRepository.GetInbox(CurrentUser).OrderByDescending(item => item.PostedDate).ToList();
                    break;
            }

            var msgsViewModels = msgs.Select(
                item => (MessageViewModel) mapper.Map(item, typeof (Message), typeof (MessageViewModel)))
                .ToPagedList(currentPageIndex, DefaultPageSize);

            ViewBag.CountNewMsgInbox = messageRepository.GetInbox(CurrentUser).Count(item => !item.IsRead);
            ViewBag.CountNewMsgOutbox = messageRepository.GetOutbox(CurrentUser).Count(item => !item.IsRead);

            return View(msgsViewModels);
        }

        /// <summary>
        ///     Создание нового сообщения
        /// </summary>
        [HttpPost]
        [Authorize]
        public ActionResult NewMessagePartial(NewMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var toUser = UserRepository.Get(model.Email);

                    var messageRepository = DependencyResolver.Current.GetService<IMessageRepository>();
                    messageRepository.SentMessage(CurrentUser, toUser, model.MsgText, model.Title);

                    return RedirectToAction("Messages", "User", new {@act = "outbox", @success = 1});
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            return View(model);
        }

        /// <summary>
        ///     Просмотр сообщения
        /// </summary>
        /// <param name="id">Уникальный идентификатор сообщения</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult ViewMessage(int? id)
        {
            if (id != null)
            {
                var messageRepository = DependencyResolver.Current.GetService<IMessageRepository>();
                var mapper = DependencyResolver.Current.GetService<IMapper>();

                var message = messageRepository.Get((int) id);

                if (!Equals(message.ToUser, CurrentUser) && !Equals(message.FromUser, CurrentUser))
                    return RedirectToAction("Messages", "User");

                if (message.ToUser.Id == CurrentUser.Id)
                {

                    message.IsRead = true;
                    messageRepository.Update(message);
                }

                var messageViewModel =
                    (MessageViewModel) mapper.Map(message, typeof (Message), typeof (MessageViewModel));

                return View(messageViewModel);
            }
            return RedirectToAction("Messages", "User");
        }

        /// <summary>
        ///     Быстрый ответ
        /// </summary>
        /// <param name="idToUser">Кому отвечаем</param>
        /// <param name="title">Заголовок</param>
        /// <param name="msgText">Тело письма</param>
        [HttpPost]
        [Authorize]
        public ActionResult QuickAnswer(int? idToUser, string title, string msgText)
        {
            if (ModelState.IsValid && idToUser != null)
            {
                try
                {
                    var toUser = UserRepository.Get((int) idToUser);

                    var messageRepository = DependencyResolver.Current.GetService<IMessageRepository>();
                    messageRepository.SentMessage(CurrentUser, toUser, msgText, title);

                    return RedirectToAction("Messages", "User");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            return View("ViewMessage");
        }

        /// <summary>
        ///     Удаление сообщения
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        [Authorize]
        public ActionResult RemoveMessage(int? id)
        {
            if (id != null)
            {
                var messageRepository = DependencyResolver.Current.GetService<IMessageRepository>();
                messageRepository.Delete((int) id);
            }

            if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
            return RedirectToAction("Messages", "User");
        }

        /// <summary>
        ///     Создание запроса на дружбу
        /// </summary>
        /// <param name="model">Кому предназначается</param>
        [HttpPost]
        [Authorize]
        public ActionResult AddFriendPartial(AddFriendViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var toUser = UserRepository.Get(model.Email);

                    var friendShipRespository = DependencyResolver.Current.GetService<IFriendShipRepository>();
                    friendShipRespository.SentRequest(CurrentUser, toUser, model.Message);

                    return RedirectToAction("Friends", "User", new { @act = "outboxrequests", @success = 1 });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", Resources.SomethingWrong);
                }
            }
            return View(model);
        }

        /// <summary>
        ///     Страница просмотра друзей, а так же входящих и исходящих заявок в друзья
        /// </summary>
        public ActionResult Friends(int? page, string act = "")
        {
            var mapper = DependencyResolver.Current.GetService<IMapper>();
            var currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            var friendshipRepository = DependencyResolver.Current.GetService<IFriendShipRepository>();

            IEnumerable<FriendShip> friendShips;
            IPagedList<FriendShipRequestViewModel> friendShipRequestViewModel;
            switch (act)
            {
                case "inboxrequests":
                    friendShips =
                        friendshipRepository.GetInboxRequests(CurrentUser).OrderByDescending(item => item.RequestDate).ToList();
                    friendShipRequestViewModel = friendShips.Select(
                        item =>
                        {
                            var temp = (FriendShipRequestViewModel)
                                mapper.Map(item.FromUser, typeof (User), typeof (FriendShipRequestViewModel));
                            temp.Message = item.Message;
                            temp.RequestDate = item.RequestDate;
                            return temp;
                        }).ToPagedList(currentPageIndex, DefaultPageSize);
                    break;
                case "outboxrequests":
                    friendShips = friendshipRepository.GetOutboxRequests(CurrentUser).OrderByDescending(item => item.RequestDate).ToList();
                    friendShipRequestViewModel = friendShips.Select(
                        item =>
                        {
                            var temp = (FriendShipRequestViewModel)
                                mapper.Map(item.ToUser, typeof (User), typeof (FriendShipRequestViewModel));
                            temp.Message = item.Message;
                            temp.RequestDate = item.RequestDate;
                            return temp;
                        }).ToPagedList(currentPageIndex, DefaultPageSize);
                    break;
                default:
                    friendShips = friendshipRepository.GetFriends(CurrentUser).ToList();
                    friendShipRequestViewModel = friendShips.Select(
                        item => (FriendShipRequestViewModel)
                            mapper.Map(item.FromUser, typeof (User), typeof (FriendShipRequestViewModel)))
                        .ToPagedList(currentPageIndex, DefaultPageSize);
                    break;
            }

            ViewBag.CountNewRequestsInbox = friendshipRepository.GetInboxRequests(CurrentUser).Count();
            ViewBag.CountNewRequestsOutbox = friendshipRepository.GetOutboxRequests(CurrentUser).Count();

            return View(friendShipRequestViewModel);
        }

        /// <summary>
        ///     Подтвердить запрос на дружбу
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public ActionResult ConfirmFriendRequest(int? id)
        {
            if (id == null)
                if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                else return RedirectToAction("Index", "Home");

            var fromUser = UserRepository.Get((int) id);
            var friendshipRepository = DependencyResolver.Current.GetService<IFriendShipRepository>();
            friendshipRepository.Confirm(fromUser, CurrentUser);

            return RedirectToAction("Index", "User", new {id});
        }

        /// <summary>
        ///     Удалить запрос на дружбу
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public ActionResult RemoveFriendRequest(int? id)
        {
            if (id == null)
                if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
                else return RedirectToAction("Index", "Home");

            var fromUser = UserRepository.Get((int) id);
            var friendshipRepository = DependencyResolver.Current.GetService<IFriendShipRepository>();
            friendshipRepository.Remove(fromUser, CurrentUser);

            if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
            return RedirectToAction("Index", "Home");
        }
    }
}