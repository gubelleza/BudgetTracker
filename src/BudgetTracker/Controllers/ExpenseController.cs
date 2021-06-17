using System;
using BudgetTracker.Models.Expenses;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    [Route("expense")]
    public class ExpenseController : Controller
    {
        private readonly IExpensesService _expensesService;

        public ExpenseController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }
        
        [HttpGet("create/id={budgetId:Guid}")]
        public IActionResult Create(Guid budgetId)
        {
            if (!IsValidBudgetId(budgetId))
                return RedirectToAction("Index", "Home");
            
            return View(_expensesService.BuildCreateExpenseViewModel(budgetId));
        }

        [HttpPost]
        public IActionResult SubmitCreate(CreateExpenseViewModel createExpenseVm)
        {
            if (_expensesService.AddExpense(createExpenseVm, ModelState))
            {
                return RedirectToAction("Display", "Budget");
            }

            return View("Create", 
                _expensesService.BuildCreateExpenseViewModel(createExpenseVm.BudgetId));
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (int.TryParse(id, out var intId))
                _expensesService.DeleteExpense(intId); 

            return RedirectToAction("Display", "Budget");
        }

        [HttpPost]
        public IActionResult Edit(ExpenseTableViewModel homeVm)
        {
            if (!_expensesService.EditExpense(homeVm, ModelState))
                TempData["ModelErrors"] =  ModelErrorsHandler.ModelStateToErrorDict(ModelState);
            
            return RedirectToAction("Display", "Budget");
        }
        
        [HttpGet("add-categories/id={budgetId:Guid}")]
        public IActionResult AddCategory(Guid budgetId)
        {
            return View(_expensesService.BuildAddCategoriesViewModel(3, budgetId));
        }

        [HttpPost]
        public IActionResult SubmitAddCategory(AddCategoriesViewModel<ExpenseCategory> addCategoriesVm)
        {
            _expensesService.AddCategories(addCategoriesVm.Categories);
            return RedirectToAction("Display", "Budget");
        }

        [HttpGet("edit-categories/id={budgetId:Guid}")]
        public IActionResult EditCategories(Guid budgetId)
        {
            if (!IsValidBudgetId(budgetId))
                return RedirectToAction("Index", "Home");
            
            return View(_expensesService.BuildEditCategoriesViewModel(budgetId));
        }

        [HttpPost]
        public IActionResult SubmitEditCategories(EditCategoriesViewModel editCategoriesVm)
        {
            _expensesService.EditCategories(editCategoriesVm);
            return RedirectToAction("Display", "Budget");
        }

        private static bool IsValidBudgetId(Guid budgetId) => budgetId != default;
    }
}