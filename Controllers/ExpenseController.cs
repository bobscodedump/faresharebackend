using FareShare.Data;
using FareShare.Entities;
using FareShare.Exceptions;
using FareShare.Models.Expense;
using Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace FareShare.Controllers;

public class ExpenseController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;

    public ExpenseController(
        DataContext context,
        UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("groupExpenses")]
    public async Task<ActionResult<List<Expense>>> GetGroupExpenses([FromQuery] string groupId)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null || userId == null)
        {
            throw new UserDoesNotExistException();
        }

        var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId);
        if (group == null)
        {
            throw new GroupDoesNotExistException();
        }

        var expenses = await _context.Expenses.Where(x => group.Id == x.GroupId).ToListAsync();

        return Ok(expenses);
    }

    [HttpGet]
    public async Task<ActionResult<Expense>> GetExpense([FromQuery] string expenseId)
    {
        var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        if (expense == null)
        {
            throw new ExpenseDoesNotExistException();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<ActionResult<Expense>> CreateExpense(NewExpenseRequest request)
    {
        var expense = new Expense()
        {
            Currency = request.Currency,
            Description = request.Description,
            GroupId = request.GroupId
        };
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
        return Ok(expense);
    }
    
    // [HttpPatch]
    // public async Task<ActionResult<Expense>> UpdateExpense(UpdateExpenseRequest request)
    // {
    //     var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == request.Id);
    //     if (expense == null)
    //     {
    //         throw new ExpenseDoesNotExistException();
    //     }
    //     
    //     expense.UpdateExpense(request.Description, request.Map);
    //     await _context.SaveChangesAsync();
    //     
    //     return Ok(expense);
    // }
}