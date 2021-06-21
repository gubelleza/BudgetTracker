using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Handlers;

namespace BudgetTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBudgetService _budgetService;
        
        public HomeController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        public IActionResult Index()
        {
            return View(_budgetService.BuildUserBudgetsViewModel(HttpContext.Session));
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}