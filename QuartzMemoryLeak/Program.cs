using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzMemoryLeak.EF;
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
                .AddTransient<IDbConnectionStringHelper, DbConnectionStringHelper>()
                .AddTransient<IJobFactory, JobFactory>()
                .AddTransient<ISchedulerFactory, StdSchedulerFactory>()
                .AddDbContext<IMyContext, MyContext>((provider, options) =>
                {
                    options.UseSqlite(provider.GetRequiredService<IDbConnectionStringHelper>().ConnectionString);
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }, ServiceLifetime.Transient, ServiceLifetime.Transient)
                ;


            var app = builder.Build();

            var myContext = app.Services.GetRequiredService<IMyContext>();
            await myContext.Database.MigrateAsync().ConfigureAwait(false);

            //var jobManager = app.Services.GetRequiredService<JobManager>();
            //await jobManager.StartJobs().ConfigureAwait(false);

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}