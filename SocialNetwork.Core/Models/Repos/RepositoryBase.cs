#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Repos
{
    public class RepositoryBase
    {
        public RepositoryBase(SocialNetworkContext db)
        {
            Db = db;
        }

        public RepositoryBase()
            : this(new SocialNetworkContext())
        {
        }

        /// <summary>
        /// Доступ к контексту базы данных
        /// </summary>
        protected SocialNetworkContext Db { get; set; }
    }
}