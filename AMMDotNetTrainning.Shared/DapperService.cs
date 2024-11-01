using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetTrainning.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection database = new SqlConnection(_connectionString);
            var list = database.Query<T>(query, param).ToList();
            return list;
        }

        public int Excute(string query, object? param = null) {
            using IDbConnection database = new SqlConnection(_connectionString);
            var result = database.Execute(query, param);
            return result;
        }

        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection database = new SqlConnection(_connectionString);
            var item = database.Query<T>(query, param).FirstOrDefault();
            return item;
        }
    }
}
