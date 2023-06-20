namespace Imagegram.Services;

using Microsoft.EntityFrameworkCore;

using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Imagegram.Exceptions.User;
using Imagegram.Models.API;

public class UserService
{
    private readonly ApiContext db = default!;
    public UserService(DbContextOptions<ApiContext> options)
    {
        db = new ApiContext(options);
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
        var NewUser = await db.AddAsync(User);
        await db.SaveChangesAsync();
        return NewUser.Entity;
    }

    public async Task<User> UpdateUser(User user)
    {
        var User = await db.Users.FindAsync(user.UserId) ?? throw new UserNotFoundException();
        User.FullName = user.FullName;
        User.UserName = user.UserName;
        await db.SaveChangesAsync();
        return User;
    }

    public async Task<User> DeleteUser(User user)
    {
        var User = await db.Users.FindAsync(user.UserId) ?? throw new UserNotFoundException();
        db.Users.Remove(User);
        await db.SaveChangesAsync();
        return User;
    }

    public async Task<User> ValidateUserCredentials(AuthCredentials userCredentials)
    {
        var User = await db.Users.Where(user => user.UserName == userCredentials.UserName &&
                                                user.Password == userCredentials.Password)
                                 .FirstOrDefaultAsync();
        return User switch
        {
            null => throw new InvalidUserCredentialsException(),
            _ => User,
        };

    }
}
