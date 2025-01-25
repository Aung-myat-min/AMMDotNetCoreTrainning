using AMMDotNetTrainning.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMMDotNetTrainning.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult ColumnChart()
        {
            var _chartData = new ColumnChartModel();
            _chartData.Title = "Monthly Inflation in Argentina, 2002";
            _chartData.Categories = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            _chartData.Data = new List<ColumnChartData>
            {
                new ColumnChartData("Inflation",
                new List<double>{ 2.3, 3.1, 4.0, 10.1, 4.0, 3.6, 3.2, 2.3, 1.4, 0.8, 0.5, 0.2 })
            };
            return View("ColumnChart", _chartData);
        }
    }
}
