using AMMDotNetCoreTrainning.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AMMDotNetCoreTrainning.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly EfCoreDbContext _db;

        public BlogsController(EfCoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().Where(x =>x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var lst = _db.TblBlogs.AsNoTracking().Where(x => x.BlogId.Equals(id) && x.DeleteFlag == false).FirstOrDefault();
            if( lst is null)
            {
                return NotFound();
            }
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlog( TblBlog blog) {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok("Successfully Created Blog!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id , TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if(item is null)
            {
                return NotFound();
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item  = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId==id && x.DeleteFlag == false);

            if (item is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle.Trim();
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor.Trim();
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent.Trim();
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id) {

            var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

            if ( blog is null)
            {
                return NotFound();
            }

            blog.DeleteFlag = true;

            _db.Entry(blog).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok();
        }
    }
}
