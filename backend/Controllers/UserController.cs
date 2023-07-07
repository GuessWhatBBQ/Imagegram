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
    private readonly ILogger<UserController> logger;
    private readonly UserService userService;

    public UserController(ILogger<UserController> logger, DbContextOptions<PostgresContext> options)
    {
        this.logger = logger;
        userService = new UserService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExistingUser>>> GetAllUsersAsync()
    {
        var users = (await userService.GetAllUsers())
            .Select(user => new ExistingUser(user))
            .ToList();
        return users;
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        try
        {
            ExistingUser user = new ExistingUser(await userService.GetUserById(id));
            return Ok(user);
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
            ExistingUser user = new ExistingUser(await userService.DeleteUser(id));
            return Ok(user);
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
            User user = await userService.GetAllPostsByUserId(id);
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<ExistingUser>> CreateAccountAsync(NewUser user)
    {
        try
        {
            var newUser = await userService.CreateNewUser(UserMapper.ToModel(user));
            return UserMapper.ResponseFromModel(newUser);
        }
        catch (UsernameAlreadyTaken)
        {
            return Problem(
                title: "Username must be unique",
                detail: $"Username '{user.UserName}' already taken",
                statusCode: StatusCodes.Status403Forbidden,
                instance: HttpContext.Request.Path
            );
        }
    }
}
