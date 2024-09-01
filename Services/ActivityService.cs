using Microsoft.EntityFrameworkCore;
using TimeTracker.Models;

public class ActivityService
{
    private readonly ApplicationDbContext context;

    public ActivityService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Activity>> GetActivities(int userId, bool activeOnly = true)
    {
        var query = context.Activities.AsQueryable();

        query = query.Where(a => a.UserId == userId);

        if (activeOnly)
        {
            query = query.Where(a => a.IsActive);
        }

        return await query.ToListAsync();
    }

    public async Task AddDefaultActivities(int userId)
    {
        var activities = new List<Activity>
        {
            new Activity
            {
                Name = "Работа",
                UserId = userId
            },
            new Activity
            {
                Name = "Спорт",
                UserId = userId
            },
            new Activity
            {
                Name = "Отдых",
                UserId = userId
            }
        };
        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}