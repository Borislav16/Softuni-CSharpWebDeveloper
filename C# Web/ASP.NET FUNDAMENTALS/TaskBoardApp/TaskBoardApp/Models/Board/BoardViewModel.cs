using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Models.Task;
using static TaskBoardApp.Data.DataConstants.Board;

namespace TaskBoardApp.Models.Board
{
    public class BoardViewModel
    {
        
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
