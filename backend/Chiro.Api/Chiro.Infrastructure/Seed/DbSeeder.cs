using Chiro.Infrastructure.Data;
using Chiro.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chiro.Infrastructure.Persistence.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChiroDbContext>();

            await context.Database.MigrateAsync();

            var seeders = new List<ISeeder>
            {
                new EventSeeder()
                // Add future seeders here:
                // new UserSeeder(),
                // new RoleSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(context);
            }
        }
    }
}