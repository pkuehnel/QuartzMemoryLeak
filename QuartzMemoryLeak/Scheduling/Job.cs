using Quartz;
using QuartzMemoryLeak.Services;

namespace QuartzMemoryLeak.Scheduling;

public class Job : IJob
{
    private readonly ILogger<Job> _logger;
    private readonly IMemoryLeakingService _memoryLeakingService;

    public Job(ILogger<Job> logger, IMemoryLeakingService memoryLeakingService)
    {
        _logger = logger;
        _memoryLeakingService = memoryLeakingService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogTrace("{method}({context})", nameof(Execute), context);
        await _memoryLeakingService.ReadData();
    }
}