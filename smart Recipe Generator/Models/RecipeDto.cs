﻿
using System.Collections.Generic;

namespace smart_Recipe_Generator.Models
{
    public class RecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();

        public NutritionInfo Nutrition { get; set; } = new();
        public List<string> Notes { get; set; } = new();
    }

    public class NutritionInfo
    {
        public string CaloriesTotal { get; set; } = string.Empty;
        public string CaloriesPerServing { get; set; } = string.Empty;
        public string ProteinPerServing { get; set; } = string.Empty;
        public string CarbsPerServing { get; set; } = string.Empty;
        public string FatPerServing { get; set; } = string.Empty;
    }
}
