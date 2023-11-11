using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MOEWiki.DB.Models;
using System.Data.Common;
using System.Diagnostics.Tracing;

namespace MOEWiki.DB.Gateways
{
    public class GatewaysFacade
    {
        private static ApplicationContext _currentContext;

        public GatewaysFacade() : this(System.Configuration.ConfigurationManager.ConnectionStrings["Test1"].ConnectionString)
        {
        }

        public GatewaysFacade(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
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
            var options = optionsBuilder.UseSqlServer("server=DESKTOP-INSKUNA\\SQLEXPRESS;database=Test1;trusted_connection=true;MultipleActiveResultSets=True;TrustServerCertificate=True").Options;
            return new ApplicationContext(options);
        }


        public static ItemGateway ItemGateway { get { return new ItemGateway(CurrentContext); } }
        public static CategoryGateway CategoryGateway { get { return new CategoryGateway(CurrentContext); } }
        public static SubcategoryGateway SubcategoryGateway { get { return new SubcategoryGateway(CurrentContext); } }
        public static ItemPropertyGateway ItemPropertyGateway { get { return new ItemPropertyGateway(CurrentContext); } }
        public static PropertyGateway PropertyGateway { get { return new PropertyGateway(CurrentContext); } }
        public static ItemRecipeGateway ItemRecipeGateway { get { return new ItemRecipeGateway(CurrentContext); } }
    }
}
