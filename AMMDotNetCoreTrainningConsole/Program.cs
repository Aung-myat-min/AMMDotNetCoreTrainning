// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

string connectionString = "Data Source=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User ID=sa;Password=sasa@123";
Console.WriteLine("Connection String: " + connectionString);

SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine("Connection Opening...");
connection.Open();
Console.WriteLine("Connection Opened!");

string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();

//adapter.Fill(dt);

SqlDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine(reader["BlogId"]);
    Console.WriteLine(reader["BlogTitle"]);
    Console.WriteLine(reader["BlogAuthor"]);
    Console.WriteLine(reader["BlogContent"]);
}

Console.WriteLine("Connection Closing...");
connection.Close();
Console.WriteLine("Connection Closed!");

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//}

Console.ReadKey();