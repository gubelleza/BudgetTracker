using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public IActionResult SubmitCreate(CreateUserViewModel createUserVm)
        {
            if (_userService.CreateUser(createUserVm, ModelState))
                return RedirectToAction("Login");

            return View("Create", createUserVm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login", new LoginViewModel());
        }

        [HttpPost]
        public IActionResult SubmitLogin(LoginViewModel loginVm)
        {
            if (_userService.Login(loginVm, ModelState, HttpContext.Session))
                return RedirectToAction("Index", "Home");
            
            return View("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _userService.Logout(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateUsername(string username)
        {
            return Json(_userService.UsernameExists(username) ? "Already taken" : "true");
        }
        
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateEmail(string email)
        {
            return Json(_userService.EmailExists(email) ? "Already taken" : "true");
        }
    }
}