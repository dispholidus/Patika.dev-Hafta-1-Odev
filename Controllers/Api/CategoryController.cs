using Microsoft.AspNetCore.Mvc;
using RestaurantMenuApi.Model;

namespace RestaurantMenuApi.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly RestaurantMenuDbContext _restaurantMenuDbContext;

        public CategoryController(ICategoryRepository categoryRepository, RestaurantMenuDbContext restaurantMenuDbContext)
        {
            _categoryRepository = categoryRepository;
            _restaurantMenuDbContext = restaurantMenuDbContext;
        }

        [HttpGet("allcategories")]
        public IActionResult GetCategories()
        {
            return Ok(_categoryRepository.GetCategories());
        }

        [HttpGet]
        public IActionResult GetCategoryFromQuery(int id)
        {
            Category? category = _categoryRepository.GetCategoryById(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryFromBody(int id)
        {
            Category? category = _categoryRepository.GetCategoryById(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        [HttpPost("add")]
        public IActionResult AddCategory(string categoryName)
        {
            var category = new Category { CategoryName = categoryName };
            if (_restaurantMenuDbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == categoryName.ToLower()) != null)
            {
                return BadRequest("Category Already Exist");
            }
            _categoryRepository.AddCategory(category);
            return Ok(category);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            if (_categoryRepository.DeleteCategoryById(id))
            {
                return Ok($"Category with id = {id} deleted.");
            }
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateCategoryById(int id, string categoryName)
        {
            Category category = new Category { CategoryId = id, CategoryName = categoryName };
            if (_categoryRepository.UpdateCategory(id, category))
            {
                return Ok(category);
            }
            return NotFound();
        }
    }
}
