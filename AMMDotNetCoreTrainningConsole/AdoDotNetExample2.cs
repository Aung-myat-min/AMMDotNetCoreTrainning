using AMMDotNetTrainning.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainningConsole
{
    class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User ID=sa;Password=sasa@123";
        private readonly AdoDotNetService _adoDotNetService;
        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                          FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            DataTable dt = _adoDotNetService.Query(query);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
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

            int result = _adoDotNetService.Excute(query, new SqlParameterModel { Name = "@title", Value = title },
                new SqlParameterModel { Name = "@author", Value = author },
                new SqlParameterModel { Name = "@content", Value = content });

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

            if (id.Length == 0)
            {
                Console.Write("Enter the blog of the ID: ");
                id = Console.ReadLine();
            }

            string query = @"
                SELECT [BlogId]
                      ,[BlogTitle]
                      ,[BlogAuthor]
                      ,[BlogContent]
                      ,[DeleteFlag]
                  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            DataTable dt = _adoDotNetService.Query(query, new SqlParameterModel { Name = "@BlogId", Value = id })


            if (dt.Rows.Count == 0)
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

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @title
                  ,[BlogAuthor] = @author
                  ,[BlogContent] = @content
                  ,[DeleteFlag] = 0
             WHERE BlogId = @id";

            int result = _adoDotNetService.Excute(query, new SqlParameterModel { Name = "@BlogId", Value = id }
               , new SqlParameterModel { Name = "@author", Value = author }
               , new SqlParameterModel { Name = "@content", Value = content }
            );

            if (result == 0)
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



            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @id";

            //to actually delete data from db
            //string query = @"DELETE FROM [dbo].[Tbl_Blog]
            //    WHERE BlogId = @id";

            int result = _adoDotNetService.Excute(query, new SqlParameterModel
            {
                Name = "id",
                Value = id
            });

            if (result == 0)
            {
                Console.WriteLine("Deleteing Blog Failed!");
            }
            else
            {
                Console.WriteLine("Successfully Deleted Blog!");
            }
        }
    }
}