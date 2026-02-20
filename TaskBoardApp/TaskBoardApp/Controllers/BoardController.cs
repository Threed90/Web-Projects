using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Data;
using TaskBoardApp.Data.ViewModels;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly TaskBoardAppDbContext _context;

        public BoardController(TaskBoardAppDbContext context)
        {
            _context = context;
        }

        public IActionResult All()
        {
            var boards = 
                _context.Boards
                .Select (t => new BoardViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Tasks = t.Tasks.Select (t => new TaskViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })
                })
                .ToList();

            return View(boards);
        }
    }
}
