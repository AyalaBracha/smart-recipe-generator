using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


using smart_Recipe_Generator.Services;
using System.Collections.Generic;

namespace smart_Recipe_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Recipe : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public Recipe(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // POST api/Recipe/generate
        [HttpPost("generate")]
        public async Task<ActionResult<RecipeDto>> GenerateRecipe([FromBody] RecipeRequestDto request)
        {
            if (request?.Ingredients == null || request.Ingredients.Count == 0)
                return BadRequest("יש לספק רשימת מוצרים.");

            var recipe = await _recipeService.GenerateRecipeAsync(request);
            return Ok(recipe);
        }
    }
}
