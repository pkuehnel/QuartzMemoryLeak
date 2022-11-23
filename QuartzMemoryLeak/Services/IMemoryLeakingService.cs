namespace QuartzMemoryLeak.Services;

public interface IMemoryLeakingService
{
    Task WriteData();
    Task ReadData();
}