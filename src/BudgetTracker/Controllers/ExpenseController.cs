using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Handlers;
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
            return View(_expensesService.BuildCreateExpenseViewModel(HttpContext.Session));
        }

        [HttpPost]
        public IActionResult SubmitCreate(CreateExpenseViewModel createExpenseVm)
        {
            if (_expensesService.AddExpense(createExpenseVm, ModelState, HttpContext.Session))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Create", _expensesService.BuildCreateExpenseViewModel(HttpContext.Session));
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (int.TryParse(id, out var intId))
                _expensesService.DeleteExpense(intId); 

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(ExpenseTableViewModel homeVm)
        {
            if (!_expensesService.EditExpense(homeVm, ModelState))
                TempData["ModelErrors"] =  ModelErrorsHandler.ModelStateToErrorDict(ModelState);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View(_expensesService.BuildAddCategoriesViewModel(5));
        }

        [HttpPost]
        public IActionResult SubmitAddCategory(AddCategoriesViewModel<ExpenseCategory> addCategoriesVm)
        {
            _expensesService.AddCategories(addCategoriesVm.Categories);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditCategories()
        {
            return View(_expensesService.BuildEditCategoriesViewModel(HttpContext.Session));
        }

        [HttpPost]
        public IActionResult SubmitEditCategories(EditCategoriesViewModel editCategoriesVm)
        {
            _expensesService.EditCategories(editCategoriesVm);
            return RedirectToAction("Index", "Home");
        }
    }
}