using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.BackgroundServices;

public class AutoMigrationBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AutoMigrationBackgroundService> _logger;

    public AutoMigrationBackgroundService(IServiceProvider serviceProvider, ILogger<AutoMigrationBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting database migration");
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await context.Database.MigrateAsync(stoppingToken);
            _logger.LogInformation("Database migration completed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during database migration");
        }
    }
}