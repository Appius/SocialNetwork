#region

using System;
using System.Data;
using System.Data.SqlClient;
using SocialNetwork.Core.Models.Abstract;

#endregion

namespace SocialNetwork.Core.Models.Repos
{
    public class DatabaseStatsRepository : RepositoryBase, IDatabaseStatsRepository
    {
        /// <summary>
        ///     Получение данных статистики базы данных
        /// </summary>
        public DbUsage GetStats()
        {
            /*Better here http://kamranicus.com/Blog/Posts/51/protip-show-db-usage-in-your-ef-application */
            var sqlConn = Db.Database.Connection as SqlConnection;
            var cmd = new SqlCommand("sp_spaceused")
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = sqlConn
                };
            var adp = new SqlDataAdapter(cmd);
            var dataset = new DataSet();

            if (sqlConn != null) sqlConn.Open();
            adp.Fill(dataset);
            if (sqlConn != null) sqlConn.Close();

            var dbUsage = new DbUsage
                {
                    database_name = dataset.Tables[0].Rows[0][0].ToString(),
                    database_size = dataset.Tables[0].Rows[0][1].ToString(),
                    unallocated_space = dataset.Tables[0].Rows[0][2].ToString(),
                    reserved = dataset.Tables[1].Rows[0][0].ToString(),
                    data = dataset.Tables[1].Rows[0][1].ToString(),
                    index_size = dataset.Tables[1].Rows[0][2].ToString(),
                    unused = dataset.Tables[1].Rows[0][3].ToString()
                };

            return dbUsage;
        }
    }
}