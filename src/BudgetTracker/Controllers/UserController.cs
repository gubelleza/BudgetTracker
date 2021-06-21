using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost("submit-create")]
        public IActionResult SubmitCreate(CreateUserViewModel createUserVm)
        {
            if (_userService.CreateUser(createUserVm, ModelState))
                return RedirectToAction("Login");

            return View("Create", createUserVm);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login", new LoginViewModel());
        }

        [HttpPost(("submit-login"))]
        public IActionResult SubmitLogin(LoginViewModel loginVm)
        {
            if (_userService.Login(loginVm, ModelState, HttpContext.Session))
                return RedirectToAction("Index", "Home");
            
            return View("Login");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            _userService.Logout(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
        
        [AcceptVerbs("GET", "POST", Route = "check-username-exists")]
        public IActionResult CheckUsernameExists(string username)
        {
            return Json(!_userService.UsernameExists(username) ? "User don't exist" : "true");
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