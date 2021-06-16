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

        [HttpGet("{id:Guid}")]
        public IActionResult Index(Guid id)
        {
            if (TempData.TryGetValue("ModelErrors", out var errors) && 
                errors is Dictionary<string, string> errorsDict)            
                ModelErrorsHandler.PopulateModelState(ModelState, errorsDict);

            ViewData[NavOptions.KEY] = NavOptions.BUDGET;
            HttpContext.Session.SetCurrentBudgetId(id);
            return View(new HomeDisplayViewModel
            {
                ExpenseTableViewModel = _expensesService.BuildExpenseTableViewModel(id)
            });  
        }
        
        [HttpPost]
        public IActionResult Create(UserBudgetsViewModel userBudgetsVm)
        {
            _budgetService.CreateBudget(userBudgetsVm, HttpContext.Session, ModelState);
            return RedirectToAction("Index", "Home");
        }
    }
}