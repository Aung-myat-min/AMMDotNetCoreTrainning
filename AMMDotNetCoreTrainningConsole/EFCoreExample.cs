using AMMDotNetCoreTrainningConsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMDotNetCoreTrainningConsole
{
    public class EFCoreExample
    {
        public void Read()
        {
            BlogModelEFContext db = new BlogModelEFContext ();
            var data = db.Blog.Where(x => x.DeleteFlag == false).ToList();

            foreach(var item in data)
            {
                Console.WriteLine($"Blod Id: {item.BlogId}\nBlog Title: {item.BlogTitle}\nBlog Author: {item.BlogAuthor}\nBlog Content: {item.BlogContent}\n");
            }
        }

        public void Write(string title = null, string author = null, string content = null)
        {
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

            BlogModelEFContext db = new BlogModelEFContext();
            EFcoreBlogDataModel blog = new EFcoreBlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
                DeleteFlag = false
            };
            db.Add(blog);
            int result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Saving Successful!" : "Saving Failed!");
        }

        public void Edit(int id = -1)
        {
            if (id == -1)
            {
                Console.Write("Enter Id: ");
                string BId = Console.ReadLine();
                if (BId.IsNullOrEmpty())
                {
                    Console.WriteLine("Error: Id is not provided.");
                    return;
                }
                id = int.Parse(BId);
            }

            BlogModelEFContext db = new BlogModelEFContext();
            var blog = db.Blog.FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

            if(blog is null)
            {
                Console.WriteLine("Blog not found!");
                return;
            }

            Console.WriteLine($"Blod Id: {blog.BlogId}\nBlog Title: {blog.BlogTitle}\nBlog Author: {blog.BlogAuthor}\nBlog Content: {blog.BlogContent}\n");
        }

        public void Update(int id = -1, string title = null, string author = null, string content = null)
        {
            if (id == -1)
            {
                Console.Write("Enter Id: ");
                string BId = Console.ReadLine();
                if (BId.IsNullOrEmpty())
                {
                    Console.WriteLine("Error: Id is not provided.");
                    return;
                }
                id = int.Parse(BId);
            }

            Console.WriteLine("Here is the blog you want to update...");
            Edit(id);

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

            BlogModelEFContext db = new BlogModelEFContext();
            var item = db.Blog.FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

            if(item is null)
            {
                Console.WriteLine("Blog not found!");
                return;
            }

            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }

            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            db.Entry(item).State = EntityState.Modified;
            
            int result = db.SaveChanges();

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
    }
}
