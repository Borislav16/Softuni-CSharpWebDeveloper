using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;
using Tasks = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskBoardDbContext data;

        public TaskController(TaskBoardDbContext context)
        {
            data = context;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            if (!GetBoards().Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist");
            }

            string currentUserId = GetUserId();
            

            if (!ModelState.IsValid)
            {
                taskModel.Boards = GetBoards();

                return View(taskModel);
            }

            Tasks task = new Tasks()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId,
            };

            await data.Tasks.AddAsync(task);
            await data.SaveChangesAsync();

            var boards = data.Boards;

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await data
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.User.UserName,
                })
                .FirstOrDefaultAsync();

            if(task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Tasks? task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            var taskModel = new TaskFormModel
            {
                Title = task.Title,
                Description = task.Description,
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel taskModel)
        {
            Tasks? task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                Unauthorized();
            }

            if (!GetBoards().Any(b => b.Id == task.BoardId))
            {
                this.ModelState.AddModelError(nameof(task.BoardId), "Board does not exist!");
            }

            task.Title = taskModel.Title;
            task.BoardId = taskModel.BoardId;
            task.Description = taskModel.Description;

            this.data.SaveChanges();

            return RedirectToAction("All", "Board");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Tasks? task = await data.Tasks.FindAsync(id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                Unauthorized();
            }

            var taskModel = new TaskViewModel
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel taskModel)
        {
            Tasks? task = await data.Tasks.FindAsync(taskModel.Id);

            if (task == null)
            {
                BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task?.OwnerId)
            {
                return Unauthorized();
            }

            data.Tasks.Remove(task);
            data.SaveChanges();
            return RedirectToAction("All", "Board");
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private IEnumerable<TaskBoardModel> GetBoards()
        {
            return data
                .Boards
                .Select(b => new TaskBoardModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                });
        }
    }
}
