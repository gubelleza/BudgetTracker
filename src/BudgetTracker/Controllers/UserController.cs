using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static BudgetTracker.Util.Constants.ModelStateErrors;

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
                return RedirectToAction("Login", "Authentication");

            return View("Create", createUserVm);
        }
        
        [AcceptVerbs("GET", "POST", Route = "check-username-exists")]
        public IActionResult CheckUsernameExists(string username)
        {
            return Json(!_userService.UsernameExists(username) ? USER_DOESNT_EXIST : "true");
        }
        
        [AcceptVerbs("GET", "POST", Route = "validate-username")]
        public IActionResult ValidateUsername(string username)
        {
            return Json(_userService.UsernameExists(username) ? DATA_ALREADY_EXISTS : "true");
        }
        
        [AcceptVerbs("GET", "POST", Route = "validate-email")]
        public IActionResult ValidateEmail(string email)
        {
            return Json(_userService.EmailExists(email) ? DATA_ALREADY_EXISTS : "true");
        }
    }
}