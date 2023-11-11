using Microsoft.EntityFrameworkCore;

namespace MOEWiki.DBMySQL.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Call> calls { get; set; } = null!;
        public DbSet<Category> categories { get; set; } = null!;
        public DbSet<Feedback> feedbacks { get; set; } = null!;
        public DbSet<Item> items { get; set; } = null!;
        public DbSet<ItemProperty> itemproperties { get; set; } = null!;
        public DbSet<ItemPropertyWithNonActual> itempropertieswithnonactual { get; set; } = null!;
        public DbSet<ItemRecipe> itemrecipes { get; set; } = null!;
        public DbSet<ItemRecipeWithNonActual> itemrecipeswithnonactual { get; set; } = null!;
        public DbSet<Property> properties { get; set; } = null!;
        public DbSet<Subcategory> subcategories { get; set; } = null!;
        public DbSet<AutoParse> autoparses { get; set; } = null!;
        public DbSet<MapMarker> mapmarkers { get; set; } = null!;
        public DbSet<Perk> perks { get; set; } = null!;
        public DbSet<Research> researches { get; set; } = null!;
        public DbSet<MarkerGroup> markergroups { get; set; } = null!;
        public DbSet<MapMarkerToMarkerGroupRel> mapmarkertomarkergrouprels { get; set; } = null!;
        public DbSet<GuildSkill> guildskills { get; set; } = null!;
        public DbSet<GuildSkillEffect> guildskilleffects { get; set; } = null!;
        public DbSet<GuildSkillRelation> guildskillrelations { get; set; } = null!;
        public DbSet<GuildSkillRequire> guildskillrequires { get; set; } = null!;
        public DbSet<Map> maps { get; set; } = null!;
        public DbSet<MapZone> mapzones { get; set; } = null!;
        public DbSet<WikiAction> wikiactions { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public ApplicationContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasIndex(u => u.Name)
                .HasFilter("[IsDelete] = 0");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("localhost;user=root;port=3306;password=Q1w2e3r4!;database=prod1;",
                new MySqlServerVersion(new Version(8, 0)));
            
        }
    }
}
