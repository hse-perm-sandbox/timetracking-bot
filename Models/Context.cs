
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TimeTracker.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Activity> Activities { get; set; }

    public DbSet<ActivityPeriod> ActivityPeriods { get; set; } 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            optionsBuilder.UseNpgsql(configuration["DbConnectionString"]);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ChatId).IsRequired().HasMaxLength(100);

            entity.HasIndex(e => e.ChatId).IsUnique();

            entity.HasMany(e => e.Activities)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.IsTracking).IsRequired();
            entity.Property(e => e.ActiveFrom).IsRequired();
            entity.Property(e => e.ActiveTo);
            entity.Property(e => e.UserId).IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(e => e.UserId);

            entity.HasMany(e => e.ActivityPeriods)
                .WithOne(a => a.Activity)
                .HasForeignKey(a => a.ActivityId);
        });

        modelBuilder.Entity<ActivityPeriod>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.StopTime).IsRequired();
            entity.Property(e => e.ActivityId).IsRequired();

            entity.HasOne(e => e.Activity)
                .WithMany(a => a.ActivityPeriods)
                .HasForeignKey(e => e.ActivityId);
        });
    }
}