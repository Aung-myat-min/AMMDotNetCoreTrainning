using AMMDotNetCoreTrainningConsole.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainningConsole
{
    internal class DapperExample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User ID=sa;Password=sasa@123";

        public void Read()
        {
            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var data = db.Query<BlogDataModel>(query);
                foreach(var item in data)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogContent);
                    Console.WriteLine(item.BlogAuthor);
                }
            }
        }

        public void Create(string title, string content, string author)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                   ([BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent]
                   ,[DeleteFlag])
             VALUES
                   (@BlogTitle
                   ,@BlogAuthor
                   ,@BlogContent
                   ,0)";

            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel()
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Saving Successful!" : "Saving Failed.");
            }
        }

        public void Edit(int id)
        {
            string query = @"
                SELECT [BlogId]
                      ,[BlogTitle]
                      ,[BlogAuthor]
                      ,[BlogContent]
                      ,[DeleteFlag]
                  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var item = db.Query<BlogDataModel>(query, new BlogDataModel() { BlogId = id}).FirstOrDefault();

                if(item is null)
                {
                    Console.WriteLine("Blog not found!");
                    return;
                }
                Console.WriteLine("Here is the Blog you want!");
                Console.WriteLine($"Blog Title: {item.BlogTitle}");
                Console.WriteLine($"Blog Content: {item.BlogContent}");
                Console.WriteLine($"Blog Author: {item.BlogAuthor}");
            }
        }
    }
}
