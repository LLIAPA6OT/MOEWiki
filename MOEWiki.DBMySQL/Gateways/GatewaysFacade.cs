using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MOEWiki.DBMySQL.Models;
using System.Data.Common;
using System.Diagnostics.Tracing;

namespace MOEWiki.DBMySQL.Gateways
{
    public class GatewaysFacade
    {
        private static ApplicationContext _currentContext;
        private static string connStr = "localhost;user=root;port=3306;password=Q1w2e3r4!;database=prod1;";
        public GatewaysFacade() : this(connStr)
        {
        }

        public GatewaysFacade(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0))).Options;
            _currentContext = new ApplicationContext(options);
        }

        public static ApplicationContext CurrentContext
        {
            get
            {
                if (_currentContext == null)
                {
                    _currentContext = GetNewContext();
                }
                return _currentContext;
            }
        }

        public static ApplicationContext GetNewContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseMySql(connStr,
                new MySqlServerVersion(new Version(8, 0))).Options;
            return new ApplicationContext(options);
        }


        public static ItemGateway ItemGateway { get { return new ItemGateway(connStr); } }
        public static CategoryGateway CategoryGateway { get { return new CategoryGateway(connStr); } }
        public static SubcategoryGateway SubcategoryGateway { get { return new SubcategoryGateway(connStr); } }
        public static ItemPropertyGateway ItemPropertyGateway { get { return new ItemPropertyGateway(connStr); } }
        public static PropertyGateway PropertyGateway { get { return new PropertyGateway(connStr); } }
        public static ItemRecipeGateway ItemRecipeGateway { get { return new ItemRecipeGateway(connStr); } }
        public static AutoParseGateway AutoParseGateway { get { return new AutoParseGateway(connStr); } }
        public static ResearchGateway ResearchGateway { get { return new ResearchGateway(connStr); } }
        public static MapMarkerGateway MapMarkerGateway { get { return new MapMarkerGateway(connStr); } }
        public static MarkerGroupGateway MarkerGroupGateway { get { return new MarkerGroupGateway(connStr); } }
        public static MapGateway MapGateway { get { return new MapGateway(connStr); } }
        public static MapZoneGateway MapZoneGateway { get { return new MapZoneGateway(connStr); } }
        public static PerkGateway PerkGateway { get { return new PerkGateway(connStr); } }
        public static GuildSkillGateway GuildSkillGateway { get { return new GuildSkillGateway(connStr); } }
        public static WikiActionGateway WikiActionGateway { get { return new WikiActionGateway(connStr); } }
    }
}
