namespace QuartzMemoryLeak.Services;

public class MemoryLeakingService : IMemoryLeakingService
{
    private readonly ILogger<MemoryLeakingService> _logger;
    private readonly List<string> _strings;

    public MemoryLeakingService(ILogger<MemoryLeakingService> logger)
    {
        _logger = logger;
        _strings = new List<string>();

        for (int i = 0; i < 100000; i++)
        {
            _strings.Add("x");
        }
    }

    public async Task Test()
    {
        _logger.LogInformation("{method}() called", nameof(Test));
        await Task.FromResult(true);
    }
}