using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;
using MOEWiki.DB.Models.Interfaces;

namespace MOEWiki.DB.Gateways
{
    public class BaseGateway
    {
        protected ApplicationContext _context { get; set; }

        public BaseGateway(ApplicationContext context)
        {
            _context = context;
        }

    }
}
