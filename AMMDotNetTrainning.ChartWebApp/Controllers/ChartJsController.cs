using AMMDotNetTrainning.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMMDotNetTrainning.ChartWebApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult BarChart()
        {
            var data = new BarChartModel();
            data.Title = "Chart.js Bar Chart with Rounded Corners";
            data.XRowNames = new List<string> { "January", "February", "March", "April", "May", "June", "July" };
            data.Data = new List<BarChartData>
            {
                    new BarChartData("Fully Rounded", new List<int>{ 30, 50, 70, 60, 90, 42, 78 }),
                    new BarChartData("Small Radius", new List<int>{ 40, 20, 80, 55, 60, 30, 95 })
            };
            return View("BarChart", data);
        }
    }
}
