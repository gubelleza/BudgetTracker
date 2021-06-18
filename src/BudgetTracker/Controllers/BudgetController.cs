using System;
using System.Collections.Generic;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Util.Constants;
using BudgetTracker.Util.Extensions;
using BudgetTracker.Util.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IExpensesService _expensesService;

        public BudgetController(IBudgetService budgetService, IExpensesService expensesService)
        {
            _budgetService = budgetService;
            _expensesService = expensesService;
        }

        [HttpGet("budget/display/id={budgetId:Guid}")]
        public IActionResult Display(Guid budgetId)
        {
            if (TempData.TryGetValue("ModelErrors", out var errors) && 
                errors is Dictionary<string, string> errorsDict)            
                ModelErrorsHandler.PopulateModelState(ModelState, errorsDict);

            ViewData[ViewDataKeys.BUDGET_ID] = budgetId;
            return View(new HomeDisplayViewModel
            {
                ExpenseTableViewModel = _expensesService.BuildExpenseTableViewModel(budgetId)
            });  
        }
        
        [HttpPost]
        public IActionResult Create(UserBudgetsViewModel userBudgetsVm)
        {
            _budgetService.CreateBudget(userBudgetsVm, HttpContext.Session, ModelState);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddMember(HomeDisplayViewModel model)
        {
            if (_budgetService.AddMember(model.Username, model.BudgetId, ModelState))
                TempData["ModelErrors"] =  ModelErrorsHandler.ModelStateToErrorDict(ModelState);

            return RedirectToAction("Display", new { budgetId = model.BudgetId });
        }
    }
}