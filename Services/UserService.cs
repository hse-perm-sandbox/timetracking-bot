using Microsoft.EntityFrameworkCore;
using TimeTracker.Models;

public class UserService
{
    private readonly ApplicationDbContext context;

    public UserService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<User> GetUserByChatId(long chatId)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.ChatId == chatId.ToString());
    }

    public async Task<bool> HasUserByChatId(long chatId)
    {
        return await context.Users.AnyAsync(u => u.ChatId == chatId.ToString());
    }

    public async Task<User> AddUser(long chatId, string username)
    {
        var user = new User
        {
            ChatId = chatId.ToString(),
            Name = username
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }
}