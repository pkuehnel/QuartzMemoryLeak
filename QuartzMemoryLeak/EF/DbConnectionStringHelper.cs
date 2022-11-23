namespace QuartzMemoryLeak.EF;

public interface IDbConnectionStringHelper
{
    string ConnectionString { get; }
}

public class DbConnectionStringHelper : IDbConnectionStringHelper
{
    private readonly ILogger<DbConnectionStringHelper> _logger;

    public DbConnectionStringHelper(ILogger<DbConnectionStringHelper> logger)
    {
        _logger = logger;
    }

    public string ConnectionString => "Data Source=MyDatabase.db";
}