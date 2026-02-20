using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;
using TaskBoardApp.Data;
using TaskBoardApp.Data.ViewModels;

namespace TaskBoardApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskBoardAppDbContext _context;

        public TaskController(TaskBoardAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = GetTaskBoards()
            };
            return View(taskModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(TaskFormModel taskModel)
        {
            if (!GetTaskBoards().Any(b => b.Id == taskModel.BoardId))
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            string currentUserId = GetUserId();

            var task = new TaskBoardApp.Data.Entities.Task()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.UtcNow,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId
            };
            this._context.Tasks.Add(task);
            this._context.SaveChanges();

            var boards = this._context.Boards;

            return RedirectToAction("All", "Board");
        }

        public IActionResult Details(int id)
        {
            var task = this._context
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                })
                .FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var task = this._context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = GetUserId();

            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetTaskBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, TaskFormModel taskModel)
        {
            var task = this._context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = GetUserId();

            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            if(!GetTaskBoards().Any(b => b.Id == taskModel.BoardId))
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            task.Title = taskModel.Title;
            task.Description = taskModel.Description;
            task.BoardId = taskModel.BoardId;

            this._context.SaveChanges();

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var task = this._context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = GetUserId();

            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel taskModel = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            };

            return View(taskModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(TaskViewModel taskModel)
        {
            var task = this._context.Tasks.Find(taskModel.Id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = GetUserId();

            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            this._context.Tasks.Remove(task);
            this._context.SaveChanges();
            return RedirectToAction("All", "Board");
        }

        private IEnumerable<TaskBoardModel> GetTaskBoards()
         => this._context
            .Boards
            .Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name
            });

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
