using Microsoft.EntityFrameworkCore;
using QuartzMemoryLeak.EF;

namespace QuartzMemoryLeak.Services;

public class MemoryLeakingService : IMemoryLeakingService
{
    private readonly ILogger<MemoryLeakingService> _logger;
    private readonly IMyContext _context;
    private readonly List<string> _strings;

    public MemoryLeakingService(ILogger<MemoryLeakingService> logger, IMyContext context)
    {
        _logger = logger;
        _context = context;
        _strings = new List<string>();
        var guid = Guid.NewGuid().ToString();
        for (int i = 0; i < 1000; i++)
        {
            _strings.Add(guid);
        }
    }
    
    public async Task ReadData()
    {
        _logger.LogInformation("{method}() called", nameof(ReadData));
        var demoEntities = await _context.DemoEntities.ToListAsync();
    }
    
    public async Task WriteData()
    {
        _logger.LogInformation("{method}() called", nameof(WriteData));
        var demoEntities = new List<DemoEntity>();
        foreach (var s in _strings)
        {
            demoEntities.Add(new DemoEntity()
            {
                Name = s,
            });
        }
        _context.DemoEntities.AddRange(demoEntities);
        await _context.SaveChangesAsync();
    }
}