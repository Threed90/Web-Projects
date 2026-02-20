using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Globals;

namespace TaskBoardApp.Data.ViewModels
{
    public class TaskFormModel
    {
        [Required]
        [MinLength(DataConstants.Task.MinTaskTitle, ErrorMessage = "Title should be at least {1} characters long.") ]
        [MaxLength(DataConstants.Task.MaxTaskTitle, ErrorMessage = "Title should be maximum {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [MinLength(DataConstants.Task.MinTaskDescription, ErrorMessage = "Description should be at least {1} characters long.")]
        [MaxLength(DataConstants.Task.MaxTaskDescription, ErrorMessage = "Description should be maximum {1} characters long.")]
        public string Description { get; set; }

        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<TaskBoardModel> Boards { get; set; }

    }
}
