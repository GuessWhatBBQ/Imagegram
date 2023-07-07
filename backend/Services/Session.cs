namespace Imagegram.Services;

using System;
using Imagegram.Exceptions.Post;
using Imagegram.Exceptions.Session;
using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Microsoft.EntityFrameworkCore;

public class SessionService
{
    private readonly PostgresContext db = default!;

    public SessionService(DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
    }

    public async Task<IEnumerable<Session>> GetAllSessions()
    {
        return await db.Sessions.ToListAsync();
    }

    public async Task<Session> GetSessionBySessionToken(string sessionToken)
    {
        return await db.Sessions
                .Where(session => session.SessionToken == sessionToken)
                .FirstOrDefaultAsync() ?? throw new SessionNotFoundException();
    }

    public async Task<IEnumerable<Session>> GetSessionsByUserId(int userId)
    {
        return await db.Sessions.Where(session => session.UserId == userId).ToListAsync();
    }

    public async Task<Session> CreateNewSession(User user)
    {
        var newSession = await db.AddAsync(
            new Session { UserId = user.UserId, SessionToken = (Guid.NewGuid()).ToString() }
        );
        await db.SaveChangesAsync();
        return newSession.Entity;
    }

    public async Task<Session> DeleteSession(string sessionToken)
    {
        var updatedSession =
            await db.Sessions
                .Where(session => session.SessionToken == sessionToken)
                .FirstOrDefaultAsync() ?? throw new SessionNotFoundException();
        db.Sessions.Remove(updatedSession);
        await db.SaveChangesAsync();
        return updatedSession;
    }

    public async Task<IEnumerable<Session>> DeleteAllSessionsByUserId(int userId)
    {
        var deletedSessions =
            await db.Sessions.Where(session => session.UserId == userId).ToListAsync()
            ?? throw new SessionNotFoundException();
        foreach (var Session in deletedSessions)
        {
            db.Sessions.Remove(Session);
        }
        await db.SaveChangesAsync();
        return deletedSessions;
    }
}
