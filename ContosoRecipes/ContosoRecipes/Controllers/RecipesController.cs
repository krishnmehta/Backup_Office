using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoRecipes.Controllers
{
    [Route("api/[controller]")] // /api/recipes
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetRecipes([FromQuery] int count)
        {
            string[] recipes = { "Pizza", "Chinese Samosa", "Bhel" };

            if (!recipes.Any())
            {
                return NotFound();
            }
            return Ok(recipes.Take(count));
          
        }
        
        /*[HttpPost]
        public ActionResult CreateNewRecipes()
        {

        }*/

        [HttpDelete]
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;
            if (badThingsHappened) { return BadRequest(); }
            return NoContent();
        }
        
    }
}
