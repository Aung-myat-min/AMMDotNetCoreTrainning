using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace AMMDotNetCoreTrainning.Console3
{
    public interface IBlogAPI
    {
        [Get("/api/blogs")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/blogs/{id}")]
        Task<BlogModel> GetBlog(int id);

        [Post("/api/blogs")]
        Task<BlogModel> CreateBlog(BlogModel blog);

        [Put("/api/blogs/{id}")]
        Task<BlogModel> UpdateBlog(int id, BlogModel blog);

        [Patch("/api/blogs/{id}")]
        Task<BlogModel> EditBlog(int id, BlogModel blog);

        [Delete("/api/blogs/{id}")]
        Task DeleteBlog(int id);
    }

    public class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; }
    }

}
