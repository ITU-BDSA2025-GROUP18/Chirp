using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public sealed class InMemorySqlite<TContext> : IDisposable where TContext : DbContext
{
    public SqliteConnection Connection { get; }
    public DbContextOptions<TContext> Options { get; }

    // Navngivet in-memory + shared cache => deles af flere connections
    public InMemorySqlite()
    {
        Connection = new SqliteConnection("Data Source=File:chirp_test?mode=memory&cache=shared");
        Connection.Open();

        Options = new DbContextOptionsBuilder<TContext>()
            .UseSqlite(Connection)
            .Options;
    }

    public void Dispose()
    {
        try { Connection.Close(); } catch { }
        try { Connection.Dispose(); } catch { }
    }
}
