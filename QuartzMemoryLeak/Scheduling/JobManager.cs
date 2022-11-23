using Quartz.Spi;
using Quartz;

namespace QuartzMemoryLeak.Scheduling;

public class JobManager
{
    private readonly ILogger<JobManager> _logger;
    private readonly IJobFactory _jobFactory;
    private readonly ISchedulerFactory _schedulerFactory;

    private IScheduler _scheduler;


#pragma warning disable CS8618
    public JobManager(ILogger<JobManager> logger, IJobFactory jobFactory, ISchedulerFactory schedulerFactory)
#pragma warning restore CS8618
    {
        _logger = logger;
        _jobFactory = jobFactory;
        _schedulerFactory = schedulerFactory;
    }

    public async Task StartJobs()
    {
        _logger.LogTrace("{Method}()", nameof(StartJobs));
        _scheduler = _schedulerFactory.GetScheduler().GetAwaiter().GetResult();
        _scheduler.JobFactory = _jobFactory;


        var everySecondTrigger = TriggerBuilder.Create()
            .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(1)).Build();

        var job = JobBuilder.Create<Job>().Build();

        var triggersAndJobs = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>
        {
            {job, new HashSet<ITrigger> {everySecondTrigger}},
        };

        await _scheduler.ScheduleJobs(triggersAndJobs, false).ConfigureAwait(false);

        await _scheduler.Start().ConfigureAwait(false);
    }
}