using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzMemoryLeak.Scheduling;
using QuartzMemoryLeak.Services;

namespace QuartzMemoryLeak
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddSingleton<JobManager>()
                .AddTransient<Job>()
                .AddTransient<IMemoryLeakingService, MemoryLeakingService>()
                .AddTransient<IJobFactory, JobFactory>()
                .AddTransient<ISchedulerFactory, StdSchedulerFactory>()
                ;


            var app = builder.Build();

            var jobManager = app.Services.GetRequiredService<JobManager>();
            await jobManager.StartJobs().ConfigureAwait(false);

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}