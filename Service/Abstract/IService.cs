using System.Collections.Generic;
using Meal_Planner.Business_Layer;

namespace Meal_Planner.Service_Layer
{
    public interface IService
    {

        //category------------------------------------------
        Category GetCategory(string name);
        List<Category> GetCategories();
        Category GetCategoryByProduct(Product product);
        void AddCategory(Category category);
        void DeleteCategory(string name);

        // product-------------------------------------------
        List<Product> GetProductsByCategory(string name);
        Product GetProduct(string name);
        void AddProduct(Product product, string categoryName);
        void DeleteProduct(string productName, string categoryName);
        void ChangeProductCharacs(DailyRation dailyRation, Product product, int value);

        //DailyRation----------------------------------------------------
        void SaveDailyRation(DailyRation dailyRation);

        //User---------------------------------------------------
        double DailyColoriesRate(User user);
        //MealTime-------------------------------------------
        void AddProductToMealTime(Product product, MealTime mealTime);
    }
}
