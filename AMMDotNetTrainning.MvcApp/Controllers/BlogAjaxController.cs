using AMMDotNetCoreTrainning.Database.Models;
using AMMDotNetCoreTrainning.Domain.Features.Blog;
using AMMDotNetTrainning.MvcApp.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace AMMDotNetTrainning.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogAjaxController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            return View("BlogList");
        }

        public IActionResult List()
        {
            var lst = _blogService.GetTblblogs();
            return Json(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreateView()
        {
            return View("BlogCreate");
        }

        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel model)
        {
            MessageModel message;

            try
            {
                _blogService.CreateBlog(new TblBlog
                {
                    BlogTitle = model.BlogTitle,
                    BlogAuthor = model.BlogAuthor,
                    BlogContent = model.BlogContent,
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Created Successfully";

                message = new MessageModel(true, "Blog Created Successfully");
            }
            catch(Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();

                message = new MessageModel(false, ex.ToString());
            }

            return Json(message);
        }

        [ActionName("Edit")]
        public IActionResult EditBlogView(int id)
        {
            var blog = _blogService.GetTblBlog(id);
            return View("BlogEdit", blog);
        }

        [HttpPatch]
        [ActionName("Update")]
        public IActionResult UpdateBlog(int id, BlogRequestModel model)
        {
            MessageModel response;

            try
            {
                var updatedBlog = new TblBlog
                {
                    BlogId = id,
                    BlogAuthor = model.BlogAuthor,
                    BlogContent = model.BlogContent,
                    BlogTitle = model.BlogTitle,
                    DeleteFlag = false
                };
                _blogService.UpdateBlog(id, updatedBlog);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Updated!";

                response = new MessageModel(true, "Blog Updated!");
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();

                response = new MessageModel(false, ex.ToString());
            }

            return Json(response);
        }

        [HttpPatch]
        [ActionName("Delete")]
        public IActionResult BlogDelete(BlogRequestModel model)
        {
            MessageModel response;
            try
            {
                _blogService.DeleteBlog(model.BlogId);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Deleted Successfully!";

                response = new MessageModel(true, "Blog Deleted!");
            }
            catch (Exception e)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = e.Message;

                response = new MessageModel(false, e.Message);

            }

            return Json(response);
        }
    }
}
