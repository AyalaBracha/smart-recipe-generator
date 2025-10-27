
using System.Collections.Generic;

namespace smart_Recipe_Generator.Services
{
    public class RecipeService : IRecipeService
    {
        public async Task<RecipeDto> GenerateRecipeAsync(RecipeRequestDto request)
        {
            // כאן אפשר להוסיף קריאות חיצוניות בעתיד
            await Task.Yield();
            var preferences = new List<string>();
            if (request.Vegetarian) preferences.Add("צמחוני");
            if (request.Vegan) preferences.Add("טבעוני");
            if (request.GlutenFree) preferences.Add("ללא גלוטן");
            if (request.MaxCalories.HasValue) preferences.Add($"מקסימום קלוריות: {request.MaxCalories}");
            if (request.Servings.HasValue) preferences.Add($"מנות: {request.Servings}");

            return new RecipeDto
            {
                Title = $"מתכון עם: {string.Join(", ", request.Ingredients ?? new List<string>())} ({string.Join(", ", preferences)})",
                Ingredients = request.Ingredients,
                Instructions = "ערבב הכל ובתיאבון!",
                Nutrition = "קלוריות: 200, חלבון: 10 גרם",
                ImageUrl = "https://via.placeholder.com/300x200.png?text=Recipe"
            };
        }
    }

    public class RecipeDto
    {
        public string? Title { get; set; }
        public List<string>? Ingredients { get; set; }
        public string? Instructions { get; set; }
        public string? Nutrition { get; set; }
        public string? ImageUrl { get; set; }
    }
}
