using System.Diagnostics;
using DemoExercises.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoExercises.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Hello, World!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "This is an ASP.NET Core MVC app.";
            ViewBag.SubMessage = "Use this area to provide additional information...";

            return View();
        }

        public IActionResult Numbers()
        {
            return View();
        }

        public IActionResult NumsToN(int count = 3)
        {
            return View(count);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
