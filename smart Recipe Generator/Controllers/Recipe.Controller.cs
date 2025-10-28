

using Microsoft.AspNetCore.Mvc;
using smart_Recipe_Generator.Models;
using smart_Recipe_Generator.Services;

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
    public async Task<ActionResult<smart_Recipe_Generator.Models.RecipeDto>> GenerateRecipe([FromBody] RecipeRequestDto request)
    {
        if (request?.Ingredients == null || request.Ingredients.Count == 0)
            return BadRequest("יש לספק רשימת מוצרים.");

        var recipe = await _recipeService.GenerateRecipeAsync(request);
        return Ok(recipe);
    }
}

