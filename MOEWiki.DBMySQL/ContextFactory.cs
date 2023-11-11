using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOEWiki.DBMySQL.Models;
using MOEWiki.DBMySQL.Gateways;

namespace MOEWiki.DBMySQL
{
    public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseMySql("localhost;user=root;port=3306;password=Q1w2e3r4!;database=prod1;",
                new MySqlServerVersion(new Version(8, 0)));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
