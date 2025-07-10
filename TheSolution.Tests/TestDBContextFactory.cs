using Microsoft.EntityFrameworkCore;
using TheSolution.Infrastructure.Data;

namespace TheSolution.Tests
{
    public static class TestDBContextFactory
    {
        public static TheSolutionDBContext Create()
        {
            var options = new DbContextOptionsBuilder<TheSolutionDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TheSolutionDBContext(options);
        }
    }
}
