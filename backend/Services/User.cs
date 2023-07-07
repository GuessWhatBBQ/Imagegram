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
            var newUser = await db.AddAsync(User);
            newUser.Entity.Password = BC.HashPassword(User.Password, workFactor: BCryptConfig.Cost);
            await db.SaveChangesAsync();
            return newUser.Entity;
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
        var updatedUser = await db.Users.FindAsync(user.UserId) ?? throw new UserNotFoundException();
        updatedUser.FullName = user.FullName;
        updatedUser.UserName = user.UserName;
        updatedUser.Password = user.Password;
        await db.SaveChangesAsync();
        return updatedUser;
    }

    public async Task<User> DeleteUser(int id)
    {
        var deletedUser = await db.Users.FindAsync(id) ?? throw new UserNotFoundException();
        db.Users.Remove(deletedUser);
        await db.SaveChangesAsync();
        return deletedUser;
    }

    public async Task<User> ValidateUserCredentials(AuthCredentials userCredentials)
    {
        var user = await db.Users
            .Where(user => user.UserName == userCredentials.UserName)
            .FirstOrDefaultAsync();
        return user switch
        {
            null => throw new InvalidUserCredentialsException(),
            _
                => isValidPassword(user, userCredentials) switch
                {
                    true => user,
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
