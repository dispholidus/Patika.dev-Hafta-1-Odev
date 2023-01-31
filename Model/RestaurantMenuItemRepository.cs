using Microsoft.EntityFrameworkCore;

namespace RestaurantMenuApi.Model
{
    public class RestaurantMenuItemRepository : IRestaurantMenuItemRepository
    {
        private readonly RestaurantMenuDbContext _restaurantMenuDbContext;

        public RestaurantMenuItemRepository(RestaurantMenuDbContext restaurantMenuDbContext)
        {
            _restaurantMenuDbContext = restaurantMenuDbContext;
        }
        public void AddRestaurantMenuItem(RestaurantMenuItem restaurantMenuItem)
        {
            _restaurantMenuDbContext.Add(restaurantMenuItem);
            _restaurantMenuDbContext.SaveChanges();
        }
        public bool DeleteRestaurantMenuItemById(int restaurantMenuItemId)
        {
            RestaurantMenuItem? restaurantMenuItemToRemove = _restaurantMenuDbContext.RestaurantMenuItems.FirstOrDefault(r => r.RestaurantMenuItemId == restaurantMenuItemId);
            if (restaurantMenuItemToRemove != null)
            {
                _restaurantMenuDbContext.Remove(restaurantMenuItemToRemove);
                _restaurantMenuDbContext.SaveChanges();
                return true;
            }
            return false;

        }
        public IEnumerable<RestaurantMenuItem> GetAllRestaurantMenuItem()
        {
            return _restaurantMenuDbContext.RestaurantMenuItems.OrderBy(r => r.RestaurantMenuItemId);
        }

        public RestaurantMenuItem? GetRestaurantMenuItemById(int restaurantMenuItemId)
        {
            return _restaurantMenuDbContext.RestaurantMenuItems.FirstOrDefault(r => r.RestaurantMenuItemId == restaurantMenuItemId);
        }
        public bool UpdateRestaurantMenuItemById(int RestaurantMenuItemId, RestaurantMenuItem newRestaurantMenuItem)
        {
            RestaurantMenuItem? restaurantMenuItemToUpdate = _restaurantMenuDbContext.RestaurantMenuItems.FirstOrDefault(r => r.RestaurantMenuItemId == RestaurantMenuItemId);
            if (restaurantMenuItemToUpdate != null)
            {
                restaurantMenuItemToUpdate.RestaurantMenuItemName = newRestaurantMenuItem.RestaurantMenuItemName;
                restaurantMenuItemToUpdate.RestaurantMenuItemDescription = newRestaurantMenuItem.RestaurantMenuItemDescription;
                restaurantMenuItemToUpdate.Price = newRestaurantMenuItem.Price;
                restaurantMenuItemToUpdate.Ingredients = newRestaurantMenuItem.Ingredients;
                restaurantMenuItemToUpdate.CategoryId = newRestaurantMenuItem.CategoryId;
                _restaurantMenuDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateRestaurantMenuItemNameById(int RestaurantMenuItemId, string newName)
        {
            RestaurantMenuItem? restaurantMenuItemToUpdate = _restaurantMenuDbContext.RestaurantMenuItems.FirstOrDefault(r => r.RestaurantMenuItemId == RestaurantMenuItemId);
            if (restaurantMenuItemToUpdate != null)
            {
                restaurantMenuItemToUpdate.RestaurantMenuItemName = newName;
                _restaurantMenuDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
