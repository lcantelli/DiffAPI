using DiffAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiffAPI
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        // ReSharper disable once UnusedMember.Global
        public DbSet<Json> Json { get; set; }
    }
}
