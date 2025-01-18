using AMMDotNetCoreTrainning.Domain.Features.Blog;
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
    }
}
