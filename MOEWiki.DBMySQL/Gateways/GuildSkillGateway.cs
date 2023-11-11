using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;
using System.Collections.Generic;

namespace MOEWiki.DBMySQL.Gateways
{
    public class GuildSkillGateway : BaseGateway
    {
        public GuildSkillGateway(string connectionString) : base(connectionString) { }

        public GuildSkill GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskills.AsNoTracking().First(f => f.Id == id);
        }
        public GuildSkill GetByIdIncludeReqAndRel(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskills.AsNoTracking().Include(i => i.GuildSkillRequires).Include(i => i.GuildSkillRelations).First(f => f.Id == id);
        }
        public GuildSkillRequire GetReq(int gsId, int reqLvl)
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskillrequires.AsNoTracking().FirstOrDefault(f => f.GuildSkillId == gsId && f.Level == reqLvl);
        }
        public List<GuildSkill> GetForFilterById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return GetParents(new List<GuildSkill>() { GetByIdIncludeReqAndRel(id) });
        }
        private List<GuildSkill> GetParents(List<GuildSkill> list)
        {
            using (var _context = new ApplicationContext(options))
            {
                var parentIds = list.SelectMany(s => s.GuildSkillRelations).Select(s => s.PrevGuildSkillId).Distinct();
                if (!parentIds.Any()) return list;
                list.AddRange(GetParents(_context.guildskills.AsNoTracking().Include(i => i.GuildSkillRelations).Include(i => i.GuildSkillRequires).Where(w => parentIds.Contains(w.Id)).ToList()));
                return list;
            }
        }

        public GuildSkill GetByName(string name)
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskills.AsNoTracking().FirstOrDefault(f => f.Name == name);
        }
        public List<GuildSkill> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskills.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<GuildSkill> GetAllForView()
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskills.AsNoTracking().Where(w => !w.IsDelete).Include(i => i.GuildSkillEffects).Include(i => i.GuildSkillRelations).ToList();
        }
        public List<GuildSkillRequire> GetAllRequires()
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskillrequires.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<GuildSkillRequire> GetAllRequiresByGSId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskillrequires.AsNoTracking().Where(w => !w.IsDelete && w.GuildSkillId == id).ToList();
        }
        public List<GuildSkillRelation> GetAllRelations()
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskillrelations.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<GuildSkillRelation> GetAllRelationsByGSId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskillrelations.AsNoTracking().Where(w => !w.IsDelete && w.GuildSkillId == id).ToList();
        }
        public List<GuildSkill> GetAllWithDeleted()
        {
            using (var _context = new ApplicationContext(options))
                return _context.guildskills.AsNoTracking().ToList();
        }
    }
}
