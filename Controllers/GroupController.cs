using FareShare.Data;
using FareShare.Exceptions;
using FareShare.Models.Group;
using Identity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FareShare.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class GroupController : ControllerBase
{
    private readonly DataContext _context;
    private UserManager<User> _userManager;

    public GroupController(
        DataContext context,
        UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("create")]
    public async Task<ActionResult<User>> CreateGroup(NewGroupRequest request)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        
        if (user == null || userId == null)
        {
            throw new UserDoesNotExistException();
        }
        
        var group = new Group()
        {
            Description = request.Description,
            Title = request.Title
        };
        group.AddUser(user);
        user.AddGroup(group);
        
        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();
        return Ok(group.Id);
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<Group>>> GetGroups()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        
        if (user == null || userId == null)
        {
            throw new UserDoesNotExistException();
        }
        
        var groups = await _context.Groups.Where(x => x.Users.Contains(user)).ToListAsync();
        return Ok(groups);
    }
    
    [HttpGet]
    public async Task<ActionResult<Group>> GetGroup([FromQuery] string groupId)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        
        if (user == null || userId == null)
        {
            throw new UserDoesNotExistException();
        }
        
        var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId);
        return Ok(group);
    }
    
}