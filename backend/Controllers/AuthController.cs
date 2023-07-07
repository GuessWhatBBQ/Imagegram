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
    private readonly ILogger<UserController> logger;
    private readonly SessionService sessionService;
    private readonly UserService userService;
    private readonly HttpClient client;

    public AuthController(ILogger<UserController> logger, DbContextOptions<PostgresContext> options)
    {
        this.logger = logger;
        sessionService = new SessionService(options);
        userService = new UserService(options);
        client = new HttpClient();
    }

    [HttpPost("")]
    public async Task<ActionResult<Session>> Login(AuthCredentials credentials)
    {
        try
        {
            var user = await userService.ValidateUserCredentials(credentials);
            Session newSession = await sessionService.CreateNewSession(User);
            Response.Headers.Add(CustomHeaderNames.XSessionId, NewSession.SessionToken);
            return Ok();
        }
        catch (InvalidUserCredentialsException)
        {
            return Unauthorized();
        }
    }

    [HttpDelete("all")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<Session>> LogoutAllSessions(int id)
    {
        Claim userIdClaim = User.Claims
            .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
            .First();
        int userId = int.Parse(userIdClaim.Value);
        try
        {
            IEnumerable<Session> sessions = await sessionService.DeleteAllSessionsByUserId(userId);
            return Ok(sessions);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<Session>> LogoutCurrentSession(int id)
    {
        Claim sessionTokenClaim = User.Claims
            .Where(claim => claim.Type == CustomClaimTypes.SessionToken)
            .First();
        string sessionToken = sessionTokenClaim.Value;
        try
        {
            Session session = await sessionService.DeleteSession(sessionToken);
            return Ok(session);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }
}
