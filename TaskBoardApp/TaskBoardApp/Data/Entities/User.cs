using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Globals;

namespace TaskBoardApp.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(DataConstants.User.MaxUserFirstName)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(DataConstants.User.MaxUserLastName)]
        public string LastName { get; set; }
    }
}
