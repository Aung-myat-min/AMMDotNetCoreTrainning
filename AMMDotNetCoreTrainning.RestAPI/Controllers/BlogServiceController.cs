using AMMDotNetCoreTrainning.Database.Models;
using AMMDotNetCoreTrainning.Domain.Features.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMMDotNetCoreTrainning.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogServiceController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _service.GetTblblogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var lst = _service.GetTblBlog(id);
            if (lst is null)
            {
                return NotFound();
            }
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var createdBlog = _service.CreateBlog(blog);
            return Ok(createdBlog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _service.UpdateBlog(id, blog);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _service.EditBlog(id, blog);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {

            var blog = _service.DeleteBlog(id);

            if (blog is null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
