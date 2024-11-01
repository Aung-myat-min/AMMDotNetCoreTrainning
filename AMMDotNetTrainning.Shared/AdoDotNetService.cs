using Microsoft.Data.SqlClient;
using System.Data;

namespace AMMDotNetTrainning.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (SqlParameterModel sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            return dt;
        }

        public int Excute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (SqlParameterModel sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
