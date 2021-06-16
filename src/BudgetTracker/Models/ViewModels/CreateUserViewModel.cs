using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [MinLength(3)]
        [Remote(action:"ValidateUsername", controller:"User", ErrorMessage = "Already taken")]
        public string Username { get; set; }
        
        [MinLength(6)]
        public string Password { get; set; }
        
        [MinLength(6)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        
        [EmailAddress]
        [Remote(action:"ValidateEmail", controller:"User", ErrorMessage = "Already taken")]
        public string Email { get; set; }
        
        [EmailAddress]
        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; }

        public void ResetPassword()
        {
            Password = null;
            ConfirmPassword = null;
        }
    }
}