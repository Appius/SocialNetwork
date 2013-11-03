#region

using System;

#endregion

namespace SocialNetwork.Core.Models.Abstract
{
    public interface IDatabaseStatsRepository
    {
        /// <summary>
        ///     Получение данных статистики базы данных
        /// </summary>
        DbUsage GetStats();
    }
}