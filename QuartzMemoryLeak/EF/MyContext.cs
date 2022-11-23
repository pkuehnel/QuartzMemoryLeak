using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace QuartzMemoryLeak.EF;

public interface IMyContext
{
    DbSet<DemoEntity> DemoEntities { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}

public class MyContext : DbContext, IMyContext
{
    public MyContext(DbContextOptions<MyContext> options)
        :base(options)
    {
        
    }

    public virtual DbSet<DemoEntity> DemoEntities { get; set; }
}

public class DemoEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}