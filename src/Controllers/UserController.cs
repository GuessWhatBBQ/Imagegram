using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using Imagegram.Repositories;
using Imagegram.Models.API;
using Imagegram.Models.Entity;
using Imagegram.Services;
using Imagegram.Exceptions.User;

namespace Imagegram.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService UserService;

    public UserController(ILogger<UserController> logger, DbContextOptions<PostgresContext> options)
    {
        _logger = logger;
        UserService = new UserService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExistingUser>>> GetAllUsersAsync()
    {
        var Users = (await UserService.GetAllUsers())
            .Select(user => new ExistingUser(user))
            .ToList();
        return Users;
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        try
        {
            ExistingUser User = new ExistingUser(await UserService.GetUserById(id));
            return Ok(User);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<User>> DeleteUserAsync(int id)
    {
        try
        {
            ExistingUser User = new ExistingUser(await UserService.DeleteUser(id));
            return Ok(User);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}/post")]
    public async Task<ActionResult<User>> GetAllPostsByUserAsync(int id)
    {
        try
        {
            User User = await UserService.GetAllPostsByUserId(id);
            return Ok(User);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<ExistingUser>> CreateAccountAsync(NewUser user)
    {
        var NewUser = await UserService.CreateNewUser(UserMapper.ToModel(user));
        return UserMapper.ResponseFromModel(NewUser);
    }
}
