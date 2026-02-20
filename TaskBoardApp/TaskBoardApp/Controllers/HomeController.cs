using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Data.ViewModels;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext _context;

        public HomeController(TaskBoardAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var taskBoards = this._context
                .Boards.Select(b => b.Name)
                .Distinct()
                .ToList();

            var tasksCounts = new List<HomeBoardModel>();

            foreach (var boardName in taskBoards)
            {
                var tasksInBoard = this._context
                    .Tasks
                    .Include(t => t.Board)
                    .Where(t => t.Board.Name == boardName)
                    .Count();

                tasksCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if(User?.Identity?.IsAuthenticated ?? false)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = this._context
                    .Tasks
                    .Where(t => t.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = this._context.Tasks.Count(),
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
