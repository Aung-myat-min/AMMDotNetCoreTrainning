﻿using AMMDotNetCoreTrainningConsole.Models;
using AMMDotNetTrainning.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainningConsole
{
    internal class DapperExample2
    {
        private readonly string _connectionString = "Data Source=DESKTOP-KPCHONN\\SQLEXPRESS;Initial Catalog=DotNetTrainning;User ID=sa;Password=sasa@123";
        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }
        public void Read()
        {

            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var data = _dapperService.Query<BlogDataModel>(query);
            foreach (var item in data)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine(item.BlogAuthor);
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


            int result = _dapperService.Excute(query, new BlogDataModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });

            Console.WriteLine(result == 1 ? "Saving Successful!" : "Saving Failed.");

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

            var item = _dapperService.Query<BlogDataModel>(query, new BlogDataModel() { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("Blog not found!");
                return;
            }
            Console.WriteLine("Here is the Blog you want!");
            Console.WriteLine($"Blog Title: {item.BlogTitle}");
            Console.WriteLine($"Blog Content: {item.BlogContent}");
            Console.WriteLine($"Blog Author: {item.BlogAuthor}");

        }

        public void Update(int id = -1, string title = null, string author = null, string content = null)
        {
            if (id == -1)
            {
                Console.WriteLine("Enter Blog Id: ");
                string BId = Console.ReadLine();
                if (BId.IsNullOrEmpty())
                {
                    Console.WriteLine("Empty Id!");
                    return;
                }
                id = int.Parse(BId);
            }

            if (title.IsNullOrEmpty())
            {
                Console.WriteLine("Enter Blog Title: ");
                string BTitle = Console.ReadLine();
                if (BTitle is null)
                {
                    Console.WriteLine("Empty Title!");
                    return;
                }
                title = BTitle.Trim();
            }

            if (author.IsNullOrEmpty())
            {
                Console.WriteLine("Enter Blog Author: ");
                string BAuthor = Console.ReadLine();
                if (BAuthor is null)
                {
                    Console.WriteLine("Empty Author!");
                    return;
                }
                author = BAuthor.Trim();
            }

            if (content.IsNullOrEmpty())
            {
                Console.WriteLine("Enter Blog Content: ");
                string BContent = Console.ReadLine();
                content = BContent;
            }

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
             WHERE BlogId = @BlogId";


            int result = _dapperService.Excute(query, new BlogDataModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });

            if (result == 0)
            {
                Console.WriteLine("Error Updating the Blog.");
            }
            else
            {
                Console.WriteLine("This is the updated blog.");
                Edit(id);
            }

        }

        public void Delete(int id = -1)
        {
            if (id == -1)
            {
                Console.WriteLine("Enter Id: ");
                string BId = Console.ReadLine();
                if (BId is null)
                {
                    Console.WriteLine("Blog Id can't be null!");
                    return;
                }
                id = int.Parse(BId);
            }

            Console.WriteLine("Here is the blog you want to delete!");
            Edit(id);

            Console.Write("Do you want to continue?: (Press 'n' to cancel): ");
            string answer = Console.ReadLine();
            answer = answer.ToLower();
            if (answer == "n")
            {
                return;
            }

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @BlogId";

            //to actually delete data from db
            //string query = @"DELETE FROM [dbo].[Tbl_Blog]
            //    WHERE BlogId = @BlogId";


            int result = _dapperService.Excute(query, new BlogDataModel() { BlogId = id });

            if (result == 1)
            {
                Console.WriteLine("Delete Successful!");
            }
            else if (result == 0)
            {
                Console.WriteLine("Deleteing Failed!");
            }
        }


    }
}
