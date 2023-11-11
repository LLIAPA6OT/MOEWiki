using Microsoft.EntityFrameworkCore;

namespace MOEWiki.DB.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Call> Calls { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<ItemProperty> ItemProperties { get; set; } = null!;
        public DbSet<ItemPropertyWithNonActual> ItemPropertiesWithNonActual { get; set; } = null!;
        public DbSet<ItemRecipe> ItemRecipes { get; set; } = null!;
        public DbSet<ItemRecipeWithNonActual> ItemRecipesWithNonActual { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<Subcategory> Subcategories { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasIndex(u => u.Name)
                .HasFilter("[IsDelete] = 0");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=admin;password=BccC1ofJZIRVYwz9;database=Test1;",
                new MySqlServerVersion(new Version(8, 2)));
        }
    }
}
