namespace smart_Recipe_Generator.Services
{
    public class RecipeRequestDto
    {
        public List<string>? Ingredients { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool GlutenFree { get; set; }
        public int? MaxCalories { get; set; }
        public int? Servings { get; set; }
    }
}
