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
