namespace Imagegram.Services;

using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Imagegram.Exceptions.User;
using Imagegram.Models.API;
using Imagegram.Config.BCrypt;
using Npgsql;

public class UserService
{
    private readonly PostgresContext db = default!;

    public UserService(DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await db.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await db.Users.FindAsync(id) ?? throw new UserNotFoundException();
    }

    public async Task<User> CreateNewUser(User User)
    {
        try
        {
            var NewUser = await db.AddAsync(User);
            NewUser.Entity.Password = BC.HashPassword(User.Password, workFactor: BCryptConfig.Cost);
            await db.SaveChangesAsync();
            return NewUser.Entity;
        }
        catch (DbUpdateException exception)
        {
            if ((exception.InnerException as PostgresException).SqlState == PostgresErrorCodes.UniqueViolation)
            {
                throw new UsernameAlreadyTaken();
            }
            throw;
        }
    }

    public async Task<User> UpdateUser(User user)
    {
        var User = await db.Users.FindAsync(user.UserId) ?? throw new UserNotFoundException();
        User.FullName = user.FullName;
        User.UserName = user.UserName;
        User.Password = user.Password;
        await db.SaveChangesAsync();
        return User;
    }

    public async Task<User> DeleteUser(int id)
    {
        var User = await db.Users.FindAsync(id) ?? throw new UserNotFoundException();
        db.Users.Remove(User);
        await db.SaveChangesAsync();
        return User;
    }

    public async Task<User> ValidateUserCredentials(AuthCredentials userCredentials)
    {
        var User = await db.Users
            .Where(user => user.UserName == userCredentials.UserName)
            .FirstOrDefaultAsync();
        return User switch
        {
            null => throw new InvalidUserCredentialsException(),
            _
                => isValidPassword(User, userCredentials) switch
                {
                    true => User,
                    false => throw new InvalidUserCredentialsException(),
                },
        };
    }

    public async Task<User> GetAllPostsByUserId(int id)
    {
        var user =
            await db.Users
                .Include(user => user.Posts)
                .ThenInclude(
                    post => post.Comments.OrderByDescending(comment => comment.CreationDate).Take(2)
                )
                .Where(user => user.UserId == id)
                .FirstOrDefaultAsync() ?? throw new UserNotFoundException();
        return user;
    }

    private bool isValidPassword(User user, AuthCredentials userCredentials)
    {
        return BC.Verify(userCredentials.Password, user.Password);
    }
}
