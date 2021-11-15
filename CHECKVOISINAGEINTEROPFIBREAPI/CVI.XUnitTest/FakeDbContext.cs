using CVI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVI.XUnitTest
{
    public class FakeDbContext
    {
        /// <summary>
        /// Create 
        /// </summary>
        /// <returns></returns>
        public CviContext CreateDbContext(IConfiguration configuration)
        {
            var builder = new DbContextOptionsBuilder<CviContext>();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var bContext = builder.UseSqlServer(connectionString);
            return new CviContext(bContext.Options);
        }
    }
}
