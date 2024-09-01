namespace TimeTracker.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsTracking { get; set; } = false;
    public DateTime ActiveFrom { get; set; } = DateTime.UtcNow;
    public DateTime? ActiveTo { get; set; }
    public int UserId { get; set; }
    public bool IsActive
    {
        get { return !ActiveTo.HasValue; }
    }

    public User User { get; set; }
    public ICollection<ActivityPeriod> ActivityPeriods { get; set; }
}