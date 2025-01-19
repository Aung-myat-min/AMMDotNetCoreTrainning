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
    }
}
