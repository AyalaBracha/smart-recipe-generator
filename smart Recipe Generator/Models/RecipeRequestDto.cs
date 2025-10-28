
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smart_Recipe_Generator.Models
{
    /// <summary>
    /// אובייקט בקשה ליצירת מתכון בהתבסס על רשימת מצרכים והעדפות תזונתיות.
    /// </summary>
    public class RecipeRequestDto
    {
        /// <summary>
        /// רשימת המצרכים שהמשתמש מזין (לפחות מרכיב אחד נדרש).
        /// </summary>
        [Required(ErrorMessage = "יש להזין לפחות מצרך אחד.")]
        public List<string> Ingredients { get; set; } = new();

        /// <summary>
        /// האם המתכון אמור להיות צמחוני.
        /// </summary>
        public bool Vegetarian { get; set; }

        /// <summary>
        /// האם המתכון אמור להיות טבעוני.
        /// </summary>
        public bool Vegan { get; set; }

        /// <summary>
        /// האם המתכון אמור להיות ללא גלוטן.
        /// </summary>
        public bool GlutenFree { get; set; }

        /// <summary>
        /// הגבלה על מספר הקלוריות למנה (אם רלוונטי).
        /// </summary>
        [Range(50, 5000, ErrorMessage = "הקלוריות צריכות להיות בטווח של 50 עד 5000.")]
        public int? MaxCalories { get; set; }

        /// <summary>
        /// מספר המנות המבוקש (אם הוזן).
        /// </summary>
        [Range(1, 20, ErrorMessage = "מספר המנות צריך להיות בין 1 ל-20.")]
        public int? Servings { get; set; }
    }
}

