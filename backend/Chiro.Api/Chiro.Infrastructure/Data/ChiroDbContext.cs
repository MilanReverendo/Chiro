using Chiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chiro.Infrastructure.Data
{
    public class ChiroDbContext(DbContextOptions<ChiroDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
