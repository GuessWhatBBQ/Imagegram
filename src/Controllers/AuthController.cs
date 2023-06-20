using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using Imagegram.Repositories;
using Imagegram.Models.API;
using Imagegram.Models.Entity;
using Imagegram.Services;
using Imagegram.Exceptions.User;
using Imagegram.Headers;
using System.Security.Claims;
using Imagegram.Claims;

namespace Imagegram.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly SessionService SessionService;
    private readonly UserService UserService;
    private readonly HttpClient client;

    public AuthController(ILogger<UserController> logger, DbContextOptions<ApiContext> options)
    {
        _logger = logger;
        SessionService = new SessionService(options);
        UserService = new UserService(options);
        client = new HttpClient();
    }

    [HttpPost("")]
    public async Task<ActionResult<Session>> Login(AuthCredentials credentials)
    {
        try
        {
            var User = await UserService.ValidateUserCredentials(credentials);
            Session NewSession = await SessionService.CreateNewSession(User);
            Response.Headers.Add(CustomHeaderNames.XSessionId, NewSession.SessionToken);
            return Ok();
        }
        catch (InvalidUserCredentialsException)
        {
            return Unauthorized();
        }
    }

    [HttpDelete("all")]
    [Authorize(AuthenticationSchemes
            = nameof(CustomAuthHandler))]
    public async Task<ActionResult<Session>> LogoutAllSessions(int id)
    {
        Claim UserIdClaim = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First();
        int UserId = int.Parse(UserIdClaim.Value);
        try
        {
            IEnumerable<Session> Sessions = await SessionService.DeleteAllSessionsByUserId(UserId);
            return Ok(Sessions);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("")]
    [Authorize(AuthenticationSchemes
            = nameof(CustomAuthHandler))]
    public async Task<ActionResult<Session>> LogoutCurrentSession(int id)
    {
        Claim SessionTokenClaim = User.Claims.Where(claim => claim.Type == CustomClaimTypes.SessionToken).First();
        string SessionToken = SessionTokenClaim.Value;
        try
        {
            Session Session = await SessionService.DeleteSession(SessionToken);
            return Ok(Session);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }
}
