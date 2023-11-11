namespace MoeWiki.WebApp.Models.Item
{
    public class RecipeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Number { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsStepByStep { get; set; } = false;
    }
}
