using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = "Should be at least 3 symbols")]
        [MaxLength(50, ErrorMessage = "Should be maximum 50 symbols")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
