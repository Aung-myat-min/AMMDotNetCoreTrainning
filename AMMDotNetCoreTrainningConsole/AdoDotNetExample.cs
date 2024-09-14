using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainningConsole
{
    public class AdoDotNetExample
    {

        private readonly string connectionString = "Data Source=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User ID=sa;Password=sasa@123";

        public void Read()
        {
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
        }

        public void Write()
        {
            Console.WriteLine("Type the blog information you want to record...");
            Console.Write("Blog Title: ");
            string title = Console.ReadLine();

            Console.Write("Blog Author: ");
            string author = Console.ReadLine();

            Console.Write("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                   ([BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent]
                   ,[DeleteFlag])
             VALUES
                   (@title
                   ,@author
                   ,@content
                   ,0)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@author", author);
            cmd.Parameters.AddWithValue("@content", content);
            
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result == 0)
            {
                Console.WriteLine("Creating new blog failed!");
            }
            else
            {
                Console.WriteLine("Successfully created new blog!");
            }
        }

        public void ReadById()
        {
            Console.WriteLine("You have started a method which get the blog information by the ID.");
            Console.Write("Enter the blog of the ID: ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"
                SELECT [BlogId]
                      ,[BlogTitle]
                      ,[BlogAuthor]
                      ,[BlogContent]
                      ,[DeleteFlag]
                  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Blog Found!");
            }
            else
            {
                DataRow dr = dt.Rows[0];
                string output = $"Here is the Blog Information.\nBook Title:\t{dr["BlogTitle"]}\nBlog Author:\t{dr["BlogAuthor"]}\nBlog Content:\t{dr["BlogContent"]}";
                Console.WriteLine(output);
            }
        }
    }
}
