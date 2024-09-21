using AMMDotNetCoreTrainningConsole.Models;
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
    }
}
