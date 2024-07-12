using FareShare.Data;
using FareShare.Exceptions;
using Identity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FareShare.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;

    public UserController(
        DataContext context,
        UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    [HttpGet("groupUsers")]
    public async Task<ActionResult<List<User>>> GetGroupUsers([FromQuery] string groupId)
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

        var users = await _context.Groups
            .Where(x => x.Id == groupId)
            .SelectMany(x => x.Users)
            .ToListAsync();
        
        return Ok(users);
    }
}