

//using System.Collections.Generic;
//using smart_Recipe_Generator.Models;

//namespace smart_Recipe_Generator.Services
//{
//    public class RecipeService : IRecipeService
//    {
//        private readonly IConfiguration _config;
//        private readonly HttpClient _httpClient;

//        public RecipeService(IConfiguration config)
//        {
//            _config = config;
//            _httpClient = new HttpClient();
//        }

//        public async Task<RecipeDto> GenerateRecipeAsync(RecipeRequestDto request)
//        {
//            string apiKey = _config["Gemini:ApiKey"] ?? "";
//            if (string.IsNullOrEmpty(apiKey))
//                throw new Exception("Gemini API Key is missing in configuration.");

//            string prompt = BuildPrompt(request);

//            //var geminiRequest = new
//            //{
//            //    contents = new[] {
//            //        new {
//            //            parts = new[] {
//            //                new { text = prompt }
//            //            }
//            //        }
//            //    }
//            //};
//            var geminiRequest = new
//            {
//                contents = new[]
//    {
//        new
//        {
//            role = "user",
//            parts = new[]
//            {
//                new {
//                    text = @"בהינתן רשימת מצרכים, צור מתכון מושלם בפורמט JSON בלבד.
//אל תשתמש ב-HTML, Markdown או טבלאות. הפלט צריך להיות מבנה JSON תקין, בפורמט הבא:

//{
//  ""title"": ""שם המנה"",
//  ""description"": ""תיאור קצר"",
//  ""ingredients"": [""מרכיבים עם כמויות""],
//  ""instructions"": [""שלבים להכנה""],
//  ""nutrition"": {
//      ""calories_total"": ""סה״כ קלוריות"",
//      ""calories_per_serving"": ""קלוריות למנה"",
//      ""protein_per_serving"": ""גרם חלבון למנה"",
//      ""carbs_per_serving"": ""גרם פחמימות למנה"",
//      ""fat_per_serving"": ""גרם שומן למנה""
//  },
//  ""notes"": [""הערות נוספות או טיפים""]
//}

//הנה רשימת המצרכים שלי:
//" + string.Join(", ", request.Ingredients) + @"

//דרישות נוספות:
//- טבעוני: " + request.Vegan + @"
//- צמחוני: " + request.Vegetarian + @"
//- ללא גלוטן: " + request.GlutenFree + @"
//- קלוריות מקסימליות: " + request.MaxCalories + @"
//- מספר מנות: " + request.Servings
//                }
//            }
//        }
//    }
//            };

//            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={apiKey}");
//            httpRequest.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(geminiRequest), System.Text.Encoding.UTF8, "application/json");

//            var response = await _httpClient.SendAsync(httpRequest);
//            var json = await response.Content.ReadAsStringAsync();

//            if (!response.IsSuccessStatusCode)
//            {
//                // נחזיר את תוכן השגיאה כדי שתוכל לראות מה הבעיה
//                return new RecipeDto
//                {
//                    Title = "שגיאה מהשרת Gemini",
//                    Ingredients = request.Ingredients,
//                    Instructions = $"Status: {response.StatusCode}\n{json}",
//                    Nutrition = "",
//                    ImageUrl = ""
//                };
//            }

//            // פענוח תשובת Gemini
//            var geminiResponse = System.Text.Json.JsonDocument.Parse(json);
//            string? recipeText = geminiResponse.RootElement
//                .GetProperty("candidates")[0]
//                .GetProperty("content")
//                .GetProperty("parts")[0]
//                .GetProperty("text").GetString();

//            // אפשר להוסיף כאן עיבוד נוסף לפורמט RecipeDto
//            return new RecipeDto
//            {
//                Title = "מתכון שנוצר ע" + "י Gemini",
//                Ingredients = request.Ingredients,
//                Instructions = recipeText,
//                Nutrition = "",
//                ImageUrl = ""
//            };
//        }

//        private string BuildPrompt(RecipeRequestDto request)
//        {
//            var prompt = $"הכן לי מתכון מפורט ויפה בעברית, כולל כמויות מדויקות של כל מרכיב, הוראות הכנה, ערכים תזונתיים, שם למנה, והצג הכל בטבלה מסודרת. \n" +
//                $"רשימת מוצרים: {string.Join(", ", request.Ingredients ?? new List<string>())}. ";
//            if (request.Vegetarian) prompt += "המתכון חייב להיות צמחוני. ";
//            if (request.Vegan) prompt += "המתכון חייב להיות טבעוני. ";
//            if (request.GlutenFree) prompt += "המתכון חייב להיות ללא גלוטן. ";
//            if (request.MaxCalories.HasValue) prompt += $"סך הקלוריות לא יעלה על {request.MaxCalories}. ";
//            if (request.Servings.HasValue) prompt += $"המתכון עבור {request.Servings} מנות. ";
//            prompt += "הצג את שם המנה, רשימת מצרכים עם כמויות, הוראות הכנה, ערכים תזונתיים, והצג הכל בצורה ברורה. \n";
//            return prompt;
//        }
//    }

//    public class RecipeDto
//    {
//        public string? Title { get; set; }
//        public List<string>? Ingredients { get; set; }
//        public string? Instructions { get; set; }
//        public string? Nutrition { get; set; }
//        public string? ImageUrl { get; set; }
//    }
//}
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using smart_Recipe_Generator.Models;

namespace smart_Recipe_Generator.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public RecipeService(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public async Task<RecipeDto> GenerateRecipeAsync(RecipeRequestDto request)
        {
            string apiKey = _config["Gemini:ApiKey"] ?? "";
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("Gemini API Key is missing in configuration.");

            string prompt = BuildPrompt(request);

            var geminiRequest = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={apiKey}"
            );
            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(geminiRequest),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new RecipeDto
                {
                    Title = "שגיאה מהשרת Gemini",
                    Ingredients = request.Ingredients,
                    Instructions = new List<string> { $"Status: {response.StatusCode}\n{json}" }
                };
            }

            // פענוח תשובת Gemini
            var geminiResponse = JsonDocument.Parse(json);
            string? recipeText = geminiResponse.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            RecipeDto recipe = new RecipeDto();

            try
            {
                if (!string.IsNullOrWhiteSpace(recipeText))
                {
                    recipe = JsonSerializer.Deserialize<RecipeDto>(
                        recipeText,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    ) ?? new RecipeDto();
                }
            }
            catch (JsonException)
            {
                // במקרה שהטקסט אינו JSON תקין
                recipe.Instructions = new List<string> { recipeText ?? "לא התקבל מתכון תקין." };
            }

            recipe.Title = string.IsNullOrWhiteSpace(recipe.Title) ? "מתכון שנוצר על ידי Gemini" : recipe.Title;
            recipe.Ingredients ??= new List<string>();
            recipe.Notes ??= new List<string>();
            recipe.Nutrition ??= new NutritionInfo();

            return recipe;
        }

        private string BuildPrompt(RecipeRequestDto request)
        {
            var prompt = @"צור JSON תקין בלבד במבנה הבא (RecipeDto), **בלי HTML, בלי Markdown, בלי קידודים מיוחדים**:

{
  ""title"": ""שם המנה"",
  ""description"": ""תיאור קצר"",
  ""ingredients"": [""מרכיבים עם כמויות""],
  ""instructions"": [""שלבים להכנה""],
  ""nutrition"": {
      ""caloriesTotal"": ""סה״כ קלוריות"",
      ""caloriesPerServing"": ""קלוריות למנה"",
      ""proteinPerServing"": ""גרם חלבון למנה"",
      ""carbsPerServing"": ""גרם פחמימות למנה"",
      ""fatPerServing"": ""גרם שומן למנה""
  },
  ""notes"": [""הערות נוספות או טיפים""]
}

רשימת מצרכים: " + string.Join(", ", request.Ingredients) + @"
- טבעוני: " + request.Vegan + @"
- צמחוני: " + request.Vegetarian + @"
- ללא גלוטן: " + request.GlutenFree + @"
- קלוריות מקסימליות: " + request.MaxCalories + @"
- מספר מנות: " + request.Servings + @"

הצג את הפלט **כ־JSON נקי בלבד**, בלי `\n`, בלי ```json, בלי תווים מיוחדים נוספים.";
            return prompt;
        }

    }
}
