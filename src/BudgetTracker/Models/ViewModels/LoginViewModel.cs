using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsEmail => UsernameOrEmail.Contains("@");
    }
}