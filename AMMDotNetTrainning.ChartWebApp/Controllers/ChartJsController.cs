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

        public IActionResult BubbleChart()
        {
            var data = new BubbleChartModel();
            data.Title = "Bubble Chart Example!";
            data.Data = new List<ParentData>
            {
                new ParentData("Dataset 1")
                {
                    data = new List<BubbleChartData>
                    {
                        new BubbleChartData(10, 20, 15),
                        new BubbleChartData(30, 40, 10),
                        new BubbleChartData(50, 60, 20)
                    }
                },
                new ParentData("Dataset 2")
                {
                    data = new List<BubbleChartData>
                    {
                        new BubbleChartData(20, 30, 25),
                        new BubbleChartData(40, 50, 15),
                        new BubbleChartData(60, 70, 30)
                    }
                }
            };
            return View("BubbleChart", data);
        }

    }
}
