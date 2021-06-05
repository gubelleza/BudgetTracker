using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;

namespace BudgetTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExpensesService _expensesService;

        public HomeController(IExpensesService expensesService, ILogger<HomeController> logger)
        {
            _expensesService = expensesService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new HomeDisplayViewModel
            {
                Expenses = _expensesService.GetCurrentMonthExpenses(),
                EditExpenseViewModel = _expensesService.BuildExpenseInputViewModel()
            });
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