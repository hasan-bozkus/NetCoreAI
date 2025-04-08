using Microsoft.AspNetCore.Mvc;
using NetCoreAI20_RecipeSuggestionWithOpenAI.Models;

namespace NetCoreAI20_RecipeSuggestionWithOpenAI.Controllers
{
    public class DefaultController : Controller
    {

        private readonly OpenAIService _openAIService;

        public DefaultController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(string ingredients)
        {
            var result = await _openAIService.GetRecipeAsync(ingredients);
            ViewBag.recipe = result;
            return View();
        }
    }
}
