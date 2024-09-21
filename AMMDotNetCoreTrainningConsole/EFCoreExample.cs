using AMMDotNetCoreTrainningConsole.Models;
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
    }
}
