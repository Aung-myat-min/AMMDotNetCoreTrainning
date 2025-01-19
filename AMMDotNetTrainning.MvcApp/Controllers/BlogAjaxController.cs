using AMMDotNetCoreTrainning.Database.Models;
using AMMDotNetCoreTrainning.Domain.Features.Blog;
using AMMDotNetTrainning.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
