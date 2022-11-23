using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuartzMemoryLeak.Scheduling;
using QuartzMemoryLeak.Services;

namespace QuartzMemoryLeak.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoryLeakController : ControllerBase
    {
        private readonly IMemoryLeakingService _memoryLeakingService;
        private readonly JobManager _jobManager;

        public MemoryLeakController(IMemoryLeakingService memoryLeakingService, JobManager jobManager)
        {
            _memoryLeakingService = memoryLeakingService;
            _jobManager = jobManager;
        }

        [HttpGet]
        public Task WriteData() => _memoryLeakingService.WriteData();

        [HttpGet]
        public Task ReadData() => _memoryLeakingService.ReadData();

        [HttpGet]
        public Task StartJob() => _jobManager.StartJobs();
    }
}
