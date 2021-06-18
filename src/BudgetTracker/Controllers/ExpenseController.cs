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

        [HttpPost("submit-create")]
        public IActionResult SubmitCreate(CreateExpenseViewModel createExpenseVm)
        {
            if (_expensesService.AddExpense(createExpenseVm, ModelState))
            {
                return RedirectToAction("Display", "Budget", new { budgetId = createExpenseVm.BudgetId });
            }

            return View("Create", 
                _expensesService.BuildCreateExpenseViewModel(createExpenseVm.BudgetId));
        }

        [HttpGet("delete")]
        public IActionResult Delete(string id, Guid budgetId)
        {
            if (int.TryParse(id, out var intId))
                _expensesService.DeleteExpense(intId); 

            return RedirectToAction("Display", "Budget", new { budgetId });
        }

        [HttpPost("edit")]
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

        [HttpPost("submit-add-categories")]
        public IActionResult SubmitAddCategory(AddCategoriesViewModel<ExpenseCategory> addCategoriesVm)
        {
            addCategoriesVm.Categories.ForEach(c => c.BudgetId = addCategoriesVm.BudgetId);
            _expensesService.AddCategories(addCategoriesVm.Categories);
            return RedirectToAction("Display", "Budget", new { budgetId = addCategoriesVm.BudgetId });
        }

        [HttpGet("edit-categories/id={budgetId:Guid}")]
        public IActionResult EditCategories(Guid budgetId)
        {
            if (!IsValidBudgetId(budgetId))
                return RedirectToAction("Index", "Home");
            
            return View(_expensesService.BuildEditCategoriesViewModel(budgetId));
        }

        [HttpPost("submit-edit-categories")]
        public IActionResult SubmitEditCategories(EditCategoriesViewModel editCategoriesVm)
        {
            _expensesService.EditCategories(editCategoriesVm);
            return RedirectToAction("Display", "Budget", new { budgetId = editCategoriesVm.BudgetId });
        }

        private static bool IsValidBudgetId(Guid budgetId) => budgetId != default;
    }
}