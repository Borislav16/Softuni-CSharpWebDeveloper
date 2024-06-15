using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.TaskConstants;

namespace TaskBoardApp.Models.Task
{
    public class TaskViewModel
    {

        public int Id { get; init; }

        public string Title { get; init; } = null!;

        public string Description { get; init; } = null!;

        public string Owner { get; init; } = null!;
    }
}