using MOEWiki.DBMySQL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Models
{
    public class WikiAction : Base
    {
        public string Who { get; set; } = String.Empty;
        public string What { get; set; } = String.Empty;
    }
}
