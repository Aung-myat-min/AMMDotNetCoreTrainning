using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace AMMDotNetCoreTrainning.Console3
{
    public class RefitExample
    {
        public async void GetBlogsAndPrint()
        {
            var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
            var blogs = await refitAPI.GetBlogs();
            foreach( var blog in blogs)
            {
                Console.WriteLine($"Blog Title:\t {blog.BlogTitle}\nBy:\t\t{blog.BlogAuthor}\nContent: {blog.BlogContent}");
            }
        }
    }
}
