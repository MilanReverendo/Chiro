using Chiro.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiro.Infrastructure.Seed
{
    public interface ISeeder
    {
        Task SeedAsync(ChiroDbContext context);
    }
}
