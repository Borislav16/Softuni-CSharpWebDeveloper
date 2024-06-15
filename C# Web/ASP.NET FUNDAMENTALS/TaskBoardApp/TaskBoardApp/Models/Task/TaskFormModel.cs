using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.TaskConstants;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(TaskMaxTitle, MinimumLength = TaskMinTitle,
            ErrorMessage = "Title should be at least {2} characters long.")]
        public string Title { get; init; } = null!;

        [Required]
        [StringLength(TaskMaxDescription, MinimumLength = TaskMinDescription,
            ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; init; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; init; }

        public IEnumerable<TaskBoardModel>? Boards { get; set; }
    }
}
