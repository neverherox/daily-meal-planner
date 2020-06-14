using Meal_Planner.Business_Layer;
using Meal_Planner.Data_Access_Layer;
using System.Collections.Generic;
namespace Meal_Planner.Service_Layer
{
    public class Service : IService
    {
        static readonly ICategoryDao categoryDao = new CategoryDao();
        static readonly IProductDao productDao = new ProductDao();
        static readonly IUserDao userDao = new UserDao();
        static readonly IMealTimeDao mealTimeDao = new MealTimeDao();
        static readonly IDailyRationDao dailyRationDao = new DailyRationDao();

        public List<Category> GetCategories()
        {
           return categoryDao.GetCategories();
        }
        public Category GetCategoryByProduct(Product product)
        {
          return categoryDao.GetCategoryByProduct(product);
        }
        public List<Product> GetProductsByCategory(string name)
        {
            return productDao.GetProductsByCategory(name);
        }
        public double DailyColoriesRate(User user)
        {
            return userDao.DailyColoriesRate(user);
        }
        public void AddCategory(Category category)
        {
            categoryDao.AddCategory(category);
        }
        public Product GetProduct(string name)
        {
            return productDao.GetProduct(name);
        }
        public void DeleteCategory(string name)
        {
            categoryDao.DeleteCategory(name);
        }
        public void AddProduct(Product product, string categoryName)
        {
            productDao.AddProduct(product, categoryName);
        }
        public void DeleteProduct(string productName,string categoryName)
        {
            productDao.DeleteProduct(productName,categoryName);
        }
        public Category GetCategory(string name)
        {
            return categoryDao.GetCategory(name);
        }
        public void ChangeProductCharacs(DailyRation dailyRation, Product product, int value)
        {
            productDao.ChangeProductCharacs(dailyRation, product, value);
        }
        public void AddProductToMealTime(Product product, MealTime mealTime)
        {
            mealTimeDao.AddProduct(product, mealTime);
        }
        public void SaveDailyRation(DailyRation dailyRation)
        {
            dailyRationDao.SaveDailyRation(dailyRation);
        }
    }
}
