using System;
using BudgetTracker.Models.Transactions;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    [Route("transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }
        
        [HttpGet("create/id={budgetId:Guid}")]
        public IActionResult Create(Guid budgetId)
        {
            if (!IsValidBudgetId(budgetId))
                return RedirectToAction("Display", "Budget", new { budgetId });
            
            return View(_transactionsService.BuildAddTransactionViewModel(budgetId));
        }

        [HttpPost("submit-create")]
        public IActionResult SubmitCreate(AddTransactionViewModel createExpenseVm)
        {
            if (_transactionsService.AddTransaction(createExpenseVm, ModelState))
            {
                return RedirectToAction("Display", "Budget", new { budgetId = createExpenseVm.BudgetId });
            }

            return View("Create", 
                _transactionsService.BuildAddTransactionViewModel(createExpenseVm.BudgetId));
        }

        [HttpGet("delete")]
        public IActionResult Delete(string id, Guid budgetId)
        {
            if (int.TryParse(id, out var intId))
                _transactionsService.DeleteTransaction(intId); 

            return RedirectToAction("Display", "Budget", new { budgetId });
        }

        [HttpPost("edit")]
        public IActionResult Edit(TransactionsTableViewModel homeVm)
        {
            if (!_transactionsService.EditTransaction(homeVm, ModelState))
                TempData["ModelErrors"] =  ModelErrorsHandler.ModelStateToErrorDict(ModelState);
            
            return RedirectToAction("Display", "Budget", new { budgetId = homeVm.BudgetId });
        }
        
        [HttpGet("add-categories/id={budgetId:Guid}")]
        public IActionResult AddCategory(Guid budgetId)
        {
            return View(_transactionsService.BuildAddCategoriesViewModel(3, budgetId));
        }

        [HttpPost("submit-add-categories")]
        public IActionResult SubmitAddCategory(AddCategoriesViewModel addCategoriesVm)
        {
            addCategoriesVm.Categories.ForEach(c => c.BudgetId = addCategoriesVm.BudgetId);
            _transactionsService.AddCategories(addCategoriesVm.Categories);
            return RedirectToAction("Display", "Budget", new { budgetId = addCategoriesVm.BudgetId });
        }

        [HttpGet("edit-categories/id={budgetId:Guid}")]
        public IActionResult EditCategories(Guid budgetId)
        {
            if (!IsValidBudgetId(budgetId))
                return RedirectToAction("Display", "Budget", new { budgetId = budgetId });
            
            return View(_transactionsService.BuildEditCategoriesViewModel(budgetId));
        }

        [HttpPost("submit-edit-categories")]
        public IActionResult SubmitEditCategories(EditCategoriesViewModel editCategoriesVm)
        {
            _transactionsService.EditCategories(editCategoriesVm);
            return RedirectToAction("Display", "Budget", new { budgetId = editCategoriesVm.BudgetId });
        }

        private static bool IsValidBudgetId(Guid budgetId) => budgetId != default;
    }
}