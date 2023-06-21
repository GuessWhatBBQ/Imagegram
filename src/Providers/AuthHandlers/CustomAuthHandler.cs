using System.Security.Claims;
using System.Text.Encodings.Web;
using Imagegram.Claims;
using Imagegram.Exceptions.Session;
using Imagegram.Headers;
using Imagegram.Models.Entity;
using Imagegram.Providers.AuthHandlers.Scheme;
using Imagegram.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class CustomAuthHandler : AuthenticationHandler<CustomAuthSchemeOptions>
{
    PostgresContext db = default!;

    public CustomAuthHandler(
        IOptionsMonitor<CustomAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        DbContextOptions<PostgresContext> dboptions
    )
        : base(options, logger, encoder, clock)
    {
        db = new PostgresContext(dboptions);
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(CustomHeaderNames.XSessionId))
        {
            return AuthenticateResult.Fail("X-Session-Id Header Not Found.");
        }
        var header = Request.Headers[CustomHeaderNames.XSessionId].ToString();
        Session session = default!;
        try
        {
            session =
                await db.Sessions
                    .Where(session => session.SessionToken == header)
                    .FirstOrDefaultAsync() ?? throw new SessionNotFoundException();
        }
        catch (SessionNotFoundException)
        {
            return AuthenticateResult.Fail("Invalid Session Id");
        }
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, session.UserId.ToString()),
            new Claim(CustomClaimTypes.SessionToken, session.SessionToken)
        };
        var claimsIdentity = new ClaimsIdentity(claims, nameof(CustomAuthHandler));
        var ticket = new AuthenticationTicket(
            new ClaimsPrincipal(claimsIdentity),
            this.Scheme.Name
        );
        return AuthenticateResult.Success(ticket);
    }
}
