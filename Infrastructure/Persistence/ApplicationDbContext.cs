using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TickerQ.EntityFrameworkCore.Configurations;
namespace Infrastructure.Persistence;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<RequestEntity> Requests => Set<RequestEntity>();
    public DbSet<RequestLog> RequestLogs { get; set; } = default!;
    public DbSet<JobEntity> Jobs => Set<JobEntity>();
    public DbSet<Report> Reports => Set<Report>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RequestEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
        });

        modelBuilder.Entity<RequestLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Action).IsRequired();
            entity.HasOne(e => e.Request)
                  .WithMany(r => r.Logs)
                  .HasForeignKey(e => e.RequestId);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.ApplyConfiguration(new TimeTickerConfigurations());
        modelBuilder.ApplyConfiguration(new CronTickerConfigurations());
        modelBuilder.ApplyConfiguration(new CronTickerOccurrenceConfigurations());

    }
}
