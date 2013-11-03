#region

using System;

#endregion

namespace SocialNetwork.Core.Models
{
    /// <summary>
    ///     Получение данных статистики базы данных
    /// </summary>
    public class DbUsage
    {
        /// <summary>
        ///     Имя текущей базы данных.
        /// </summary>
        public string database_name { get; set; }

        /// <summary>
        ///     Размер текущей базы данных в мегабайтах. Значение database_size включает как файлы данных, так и файлы журналов.
        /// </summary>
        public string database_size { get; set; }

        /// <summary>
        ///     Место в базе данных, не зарезервированное для объектов базы данных.
        /// </summary>
        public string unused { get; set; }

        /// <summary>
        ///     Общий объем пространства, используемый индексами.
        /// </summary>
        public string index_size { get; set; }

        /// <summary>
        ///     Общий объем пространства, выделенный объектам в базе данных.
        /// </summary>
        public string reserved { get; set; }

        /// <summary>
        ///     Общий объем пространства, используемый данными.
        /// </summary>
        public string data { get; set; }

        /// <summary>
        ///     Общий объем пространства, зарезервированный для объектов в базе данных, но пока не используемый.
        /// </summary>
        public string unallocated_space { get; set; }
    }
}