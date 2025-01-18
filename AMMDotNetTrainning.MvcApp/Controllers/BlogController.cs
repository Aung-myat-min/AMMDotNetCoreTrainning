using AMMDotNetCoreTrainning.Domain.Features.Blog;
using AMMDotNetTrainning.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
