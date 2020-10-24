using DockerSqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerSqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
