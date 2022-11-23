using Quartz;
using QuartzMemoryLeak.Services;

namespace QuartzMemoryLeak.Scheduling;

public class Job : IJob
{
    private readonly ILogger<Job> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Job(ILogger<Job> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogTrace("{method}({context})", nameof(Execute), context);
        using var scope = _serviceScopeFactory.CreateScope();
        var memoryLeakingService = scope.ServiceProvider.GetRequiredService<IMemoryLeakingService>();
        await memoryLeakingService.ReadData();
    }
}