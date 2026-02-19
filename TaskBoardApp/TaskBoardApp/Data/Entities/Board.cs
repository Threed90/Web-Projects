using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Globals;

namespace TaskBoardApp.Data.Entities
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Board.MaxBoardName)]
        public string Name { get; set; }

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
