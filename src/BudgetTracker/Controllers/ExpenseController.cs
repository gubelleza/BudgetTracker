using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpensesService _expensesService;

        public ExpenseController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(_expensesService.BuildExpenseInputViewModel());
        }

        [HttpPost]
        public IActionResult SubmitCreate(IExpenseInputViewModel createExpenseVm)
        {
            if (_expensesService.AddExpense(createExpenseVm.ToExpense, ModelState))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Create");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (int.TryParse(id, out var intId))
                _expensesService.DeleteExpense(intId); 

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(string id)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}