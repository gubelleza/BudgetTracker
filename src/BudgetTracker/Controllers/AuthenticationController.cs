using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login", new LoginViewModel());
        }

        [HttpPost(("submit-login"))]
        public IActionResult SubmitLogin(LoginViewModel loginVm)
        {
            if (_authenticationService.Login(loginVm, ModelState, HttpContext.Session))
                return RedirectToAction("Index", "Home");
            
            return View("Login");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            _authenticationService.Logout(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
    }
}