using Forum.Globals;
using System.ComponentModel.DataAnnotations;

namespace Forum.Services.DTO
{
    public class PostViewModel
    {
        public string? Id { get; set; } = null!;

        [Required]
        [MinLength(DataConstants.Post.TitleMinLength)]
        [MaxLength(DataConstants.Post.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(DataConstants.Post.ContentMinLength)]
        [MaxLength(DataConstants.Post.ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
