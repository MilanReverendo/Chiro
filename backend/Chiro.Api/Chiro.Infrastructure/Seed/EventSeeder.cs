using Chiro.Domain.Entities;
using Chiro.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Infrastructure.Seed
{
    public class EventSeeder : ISeeder
    {
        public async Task SeedAsync(ChiroDbContext context)
        {
            if (context.Events.Any())
                return; // Already seeded

            var events = Enumerable.Range(1, 20).Select(i => new Event
            {
                Id = Guid.NewGuid(),
                Name = $"Chiro Event {i}",
                Description = $"Description for event {i}",
                StartDate = DateTime.UtcNow.AddDays(i),
                EndDate = DateTime.UtcNow.AddDays(i).AddHours(2)
            }).ToList();

            await context.Events.AddRangeAsync(events);
            await context.SaveChangesAsync();
        }
    }
}
