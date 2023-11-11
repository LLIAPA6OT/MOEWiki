using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Models;
using MOEWiki.DBMySQL.Models.Interfaces;

namespace MOEWiki.DBMySQL.Gateways
{
    public class BaseGateway
    {
        public readonly DbContextOptions<ApplicationContext> options;
        public BaseGateway(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0))).Options;
        }
    }
}
