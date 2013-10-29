#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Web.Mvc;
using MvcPaging;
using SocialNetwork.Core.Helpers;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Models.Abstract;
using SocialNetwork.Core.Models.Repos;
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
        /// Главная страница просмотра информации о пользователе
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя (0 - твоя страница)</param>
        [Authorize]
        public ActionResult Index(int id = 0)
        {
            if (id == 0) id = CurrentUser.Id;
            var user = UserRepository.Get(id);

            if (user == null) return View();

            var photoName = user.Id.ToString(CultureInfo.InvariantCulture).ComputeStringHash();
            var photoPath = Path.Combine(Server.MapPath("~/Pics"), photoName);

            if (System.IO.File.Exists(photoPath))
            {
                ViewBag.PhotoUrl = Url.Content(Path.Combine(Url.Content("~/Pics/"), photoName));
            }
            return View(user);
        }

        /// <summary>
        /// Страница просмотра сообщений
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
                    msgs = messageRepository.GetOutbox(CurrentUser).OrderByDescending(item=>item.PostedDate).ToList();
                    break;
                default:
                    msgs = messageRepository.GetInbox(CurrentUser).OrderByDescending(item => item.PostedDate).ToList();
                    break;
            }

            var msgsViewModels = msgs.Select(
                item => (MessageViewModel) mapper.Map(item, typeof (Message), typeof (MessageViewModel)))
                .ToPagedList(currentPageIndex, DefaultPageSize);

            msgsViewModels.ForEach(delegate(MessageViewModel model)
            {
                var fileName = (act == "outbox" ? model.ToUser.Id : model.FromUser.Id).ToString(CultureInfo.InvariantCulture).ComputeStringHash();
                var path = Path.Combine(Server.MapPath("~/Pics"), fileName);

                model.PhotoUrl = Url.Content(System.IO.File.Exists(path)
                        ? Path.Combine(Url.Content("~/Pics/"), fileName)
                        : Url.Content("~/Pics/no-photo.bmp"));
            });

            ViewBag.CountNewMsgInbox = messageRepository.GetInbox(CurrentUser).Count(item => !item.IsRead);
            ViewBag.CountNewMsgOutbox = messageRepository.GetOutbox(CurrentUser).Count(item => !item.IsRead);

            return View(msgsViewModels);
        }

        /// <summary>
        /// Создание нового сообщения
        /// </summary>
        /// <param name="idToUser">Кому предназначается сообщение</param>
        [HttpGet]
        [Authorize]
        public ActionResult NewMessagePartial(int? idToUser)
        {
            if (idToUser != null)
            {
                var user = UserRepository.Get((int) idToUser);

                var msg = new NewMessageViewModel
                {
                    Email = user.Email,
                    ToUser = string.Format("{0} {1}", user.FirstName, user.LastName)
                };

                return View(msg);
            }
            return View();
        }

        /// <summary>
        /// Создание нового сообщения
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

//                    if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.PathAndQuery);
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
        /// Просмотр сообщения
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

                if (message.ToUser.Id == CurrentUser.Id)
                {
                    message.IsRead = true;
                    messageRepository.Update(message);
                }

                var messageViewModel = (MessageViewModel)mapper.Map(message, typeof(Message), typeof(MessageViewModel));

                return View(messageViewModel);
            }
            return View();
        }

        /// <summary>
        /// Быстрый ответ
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
        /// Удаление сообщения
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
    }
}