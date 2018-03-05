using DiffAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiffAPI
{
    /// <summary>
    /// Database Context from Entity Framework Core
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        // ReSharper disable once UnusedMember.Global
        // JSON table
        public DbSet<Json> Json { get; set; }
    }
}
