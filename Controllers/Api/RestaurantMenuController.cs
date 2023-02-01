using Microsoft.AspNetCore.Mvc;
using RestaurantMenuApi.Model;

namespace RestaurantMenuApi.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantMenuController : Controller
    {
        private readonly IRestaurantMenuItemRepository _restaurantMenuItemRepository;
        private readonly RestaurantMenuDbContext _restaurantMenuDbContext;

        public RestaurantMenuController(IRestaurantMenuItemRepository restaurantMenuItemRepository, RestaurantMenuDbContext restaurantMenuDbContext)
        {
            _restaurantMenuItemRepository = restaurantMenuItemRepository;
            _restaurantMenuDbContext = restaurantMenuDbContext;
        }

        [HttpGet]
        public IActionResult GetRestaurantMenuItemFromQuery(int id)
        {
            RestaurantMenuItem? restauranMenuItem = _restaurantMenuItemRepository.GetRestaurantMenuItemById(id);
            if (restauranMenuItem != null)
            {
                return Ok(restauranMenuItem);
            }
            return NotFound();
        }
        [HttpGet("id")]
        public IActionResult GetRestaurantMenuItemFromBody(int id)
        {
            RestaurantMenuItem? restauranMenuItem = _restaurantMenuItemRepository.GetRestaurantMenuItemById(id);
            if (restauranMenuItem != null)
            {
                return Ok(restauranMenuItem);
            }
            return NotFound();
        }
        [HttpGet("restaurantmenuitems")]
        public IActionResult GetRestaurantMenuItems()
        {
            return Ok(_restaurantMenuItemRepository.GetAllRestaurantMenuItem());
        }

        [HttpPost]
        public IActionResult AddRestaurantMenuItem(string name, decimal price, string? restaurantMenuItemDescription, string? ingredients, int categoryId)
        {
            var newRestaurantMenuItem = new RestaurantMenuItem
            {
                RestaurantMenuItemName = name,
                Price = price,
                RestaurantMenuItemDescription = restaurantMenuItemDescription,
                Ingredients = ingredients,
                CategoryId = categoryId
            };

            if (_restaurantMenuDbContext.RestaurantMenuItems.FirstOrDefault(r => r.RestaurantMenuItemName.ToLower() == name.ToLower()) != null)
            {
                return BadRequest("Item already exist!");
            }

            _restaurantMenuItemRepository.AddRestaurantMenuItem(newRestaurantMenuItem);
            return Ok(newRestaurantMenuItem);
        }

        [HttpDelete]
        public IActionResult DeleteRestaurantMenuItemById(int id)
        {
            if (_restaurantMenuItemRepository.DeleteRestaurantMenuItemById(id))
            {
                return Ok($"Item with id = {id} deleted.");
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRestaurantMenuItemById(int id, [FromBody] RestaurantMenuItem restaurantMenuItem)
        {
            restaurantMenuItem.RestaurantMenuItemId = id;
            if (_restaurantMenuItemRepository.UpdateRestaurantMenuItemById(id, restaurantMenuItem))
            {
                return Ok($"Item with id = {id} updated.");
            }
            return BadRequest($"Item with id = {id} does not exist.");
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateRestaurantItemCategoryById(int id, string name)
        {
            if (_restaurantMenuItemRepository.UpdateRestaurantMenuItemNameById(id, name))
            {
                return Ok($"Item with id = {id} name is changed");
            }
            return BadRequest($"Item with id = {id} does not exist.");
        }
    }
}
