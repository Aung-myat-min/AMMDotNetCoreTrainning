using AMMDotNetTrainning.Console;
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

        private readonly string _connectionString = AppSettings.ConnectionString;

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
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

            SqlConnection connection = new SqlConnection(_connectionString);
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

        public void ReadById(string id = "")
        {
            Console.WriteLine("You have started a method which get the blog information by the ID.");
            
            if(id.Length == 0)
            {
                Console.Write("Enter the blog of the ID: ");
                id = Console.ReadLine();
            }

            SqlConnection connection = new SqlConnection(_connectionString);
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

        public void Update()
        {
            Console.WriteLine();
            Console.WriteLine("Instruction: 1. Find using ID 2. Edit the information.");
            Console.Write("Enter the Blog Id: ");
            string id = Console.ReadLine();

            ReadById(id);

            Console.WriteLine();
            Console.Write("Blog Title: ");
            string title = Console.ReadLine();

            Console.Write("Blog Author: ");
            string author = Console.ReadLine();

            Console.Write("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @title
                  ,[BlogAuthor] = @author
                  ,[BlogContent] = @content
                  ,[DeleteFlag] = 0
             WHERE BlogId = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@author", author);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@id", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if(result == 0)
            {
                Console.WriteLine("Updating Blog Failed!");
            }
            else
            {
                Console.WriteLine("Successfully Updated Blog!\nHere are the result!");
                Console.WriteLine();
                ReadById(id);
            }
        }

        public void Delete()
        {
            Console.WriteLine();
            Console.WriteLine("Enter the Blog Id to delete!");

            Console.WriteLine();
            Console.Write("Blog Id: ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @id";

            //to actually delete data from db
            //string query = @"DELETE FROM [dbo].[Tbl_Blog]
            //    WHERE BlogId = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result == 0) {
                Console.WriteLine("Deleteing Blog Failed!");
            }
            else
            {
                Console.WriteLine("Successfully Deleted Blog!");
            }
        }
    }
}
