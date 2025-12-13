using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TextSplitterApp.Models;

namespace TextSplitterApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(TextViewModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Text))
                model.SplittedText = string.Join(Environment.NewLine, model.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries));

            // Possibility to be done with redirection if there is a post method
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
