using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Globals;

namespace TaskBoardApp.Data.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Task.MaxTaskTitle)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.Task.MaxTaskDescription)]
        public string Description { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
