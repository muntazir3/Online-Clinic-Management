using System.ComponentModel.DataAnnotations;

namespace EPROJECT.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "New Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string? Password { get; set; }
        public bool RememberLogin { get; set; }
        public string? Returnurl { get; set; }

    }
}
