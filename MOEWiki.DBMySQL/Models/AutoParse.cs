using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Models.Interfaces;
using MOEWiki.DBMySQL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class AutoParse : Base
    {
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public ParseStateEnum ParseState { get; set; } = ParseStateEnum.Scanned;
        public ModeEnum Mode { get; set; }
        public string ParseResult { get; set; } = String.Empty;
        public string ImageName { get; set; } = String.Empty;
        public string RecognitionResult { get; set; } = String.Empty;
        public int ItemId { get; set; } = 0;
        public string AdditionalString1 { get; set; } = String.Empty;
    }
}
