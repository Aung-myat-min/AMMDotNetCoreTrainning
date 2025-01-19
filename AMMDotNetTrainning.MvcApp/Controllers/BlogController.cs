using AMMDotNetCoreTrainning.Domain.Features.Blog;
using AMMDotNetTrainning.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using AMMDotNetCoreTrainning.Database.Models;

namespace AMMDotNetTrainning.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var lst = _blogService.GetTblblogs();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel model)
        {
            try
            {
                _blogService.CreateBlog(new AMMDotNetCoreTrainning.Database.Models.TblBlog
                {
                    BlogTitle = model.BlogTitle,
                    BlogAuthor = model.BlogAuthor,
                    BlogContent = model.BlogContent,
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Created Successfully!";
            }
            catch (Exception e)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            try
            {
                _blogService.DeleteBlog(id);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Deleted Successfully!";
            }
            catch (Exception e)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var blog = _blogService.GetTblBlog(id);
            if (blog != null)
            {
                return View("EditBlog", blog);
            }
            else
            {
                return View("Index");
            }
        }

        [ActionName("Update")]
        public IActionResult BlogUpdateAction(int id, BlogRequestModel blog)
        {
            try
            {
                var updatedBlog = new TblBlog
                {
                    BlogId = id,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent,
                    BlogTitle = blog.BlogTitle,
                    DeleteFlag = false
                };
                _blogService.UpdateBlog(id, updatedBlog);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Updated Successfully!";
            }
            catch (Exception e)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
