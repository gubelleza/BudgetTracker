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
            return View("Create");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateUsername(string Username)
        {
            return Json(_userService.UsernameExists(Username) ? "false" : "true");
        }
        
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateEmail(string Email)
        {
            return Json(_userService.EmailExists(Email) ? "false" : "true");
        }
    }
}