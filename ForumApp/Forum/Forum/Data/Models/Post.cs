using Forum.Globals;
using System.ComponentModel.DataAnnotations;

namespace Forum.Data.Models
{
    public class Post
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Post.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.Post.ContentMaxLength)]
        public string Content { get; set; }
    }
}
