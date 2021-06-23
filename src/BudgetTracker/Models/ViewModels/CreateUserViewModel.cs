using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static BudgetTracker.Util.Constants.ModelStateErrors;

namespace BudgetTracker.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [MinLength(3, ErrorMessage = AT_LEAST_5_CHARS)]
        [Remote(action:"ValidateUsername", controller:"User", ErrorMessage = DATA_ALREADY_EXISTS)]
        public string Username { get; set; }
        
        [MinLength(6, ErrorMessage = AT_LEAST_6_CHARS)]
        public string Password { get; set; }
        
        [MinLength(6, ErrorMessage = AT_LEAST_6_CHARS)]
        [Compare(nameof(Password), ErrorMessage = PASSWORDS_MUST_MATCH)]
        public string ConfirmPassword { get; set; }
        
        [EmailAddress]
        [Remote(action:"ValidateEmail", controller:"User", ErrorMessage = DATA_ALREADY_EXISTS)]
        public string Email { get; set; }
        
        [EmailAddress]
        [Compare(nameof(Email), ErrorMessage = EMAILS_MUST_MATCH)]
        public string ConfirmEmail { get; set; }

        [BindNever] 
        public bool PasswordsMatch => Password == ConfirmPassword;
        
        [BindNever]
        public bool EmailsMatch => Email == ConfirmEmail;
        
        
        public void ResetPassword()
        {
            Password = null;
            ConfirmPassword = null;
        }
    }
}