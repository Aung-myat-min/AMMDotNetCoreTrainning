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
            try
            {
                var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
                var blogs = await refitAPI.GetBlogs();
                foreach (var blog in blogs)
                {
                    Console.WriteLine($"Blog Title:\t {blog.BlogTitle}\nBy:\t\t{blog.BlogAuthor}\nContent: {blog.BlogContent}");
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog Not Found");
                }
                else
                {
                    Console.WriteLine("Internal Server Error!");
                }
            }

        }

        public async void GetOneBlog(int id)
        {
            try
            {
                var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
                var blogs = await refitAPI.GetBlog(id);

                Console.WriteLine($"Blog Title:\t {blogs.BlogTitle}\nBy:\t\t{blogs.BlogAuthor}\nContent: {blogs.BlogContent}");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog Not Found");
                }
                else
                {
                    Console.WriteLine("Internal Server Error!");
                }
            }
        }

        public async void CreateBlog(BlogModel blog)
        {
            try
            {
                var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
                var blogs = await refitAPI.CreateBlog(blog);
                Console.WriteLine($"Blog Title:\t {blogs.BlogTitle}\nBy:\t\t{blogs.BlogAuthor}\nContent: {blogs.BlogContent}");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog Not Found");
                }
                else
                {
                    Console.WriteLine("Internal Server Error!");
                }
            }
        }

        public async void UpdateBlog(int id, BlogModel blog)
        {
            try
            {
                var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
                var blogs = await refitAPI.UpdateBlog(id, blog);

                Console.WriteLine($"Blog Title:\t {blogs.BlogTitle}\nBy:\t\t{blogs.BlogAuthor}\nContent: {blogs.BlogContent}");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog Not Found");
                }
                else
                {
                    Console.WriteLine("Internal Server Error!");
                }
            }
        }

        public async void EditBlog(int id, BlogModel blog)
        {
            try
            {
                var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
                var blogs = await refitAPI.EditBlog(id, blog);

                Console.WriteLine($"Blog Title:\t {blogs.BlogTitle}\nBy:\t\t{blogs.BlogAuthor}\nContent: {blogs.BlogContent}");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog Not Found");
                }
                else
                {
                    Console.WriteLine("Internal Server Error!");
                }
            }

        }

        public async void DeleteBlog(int id)
        {
            try
            {
                var refitAPI = RestService.For<IBlogAPI>("https://localhost:7033");
                await refitAPI.DeleteBlog(id);

                Console.WriteLine("Blog Deleted!");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Blog Not Found");
                }
                else
                {
                    Console.WriteLine("Internal Server Error!");
                }
            }
        }
    }
}
