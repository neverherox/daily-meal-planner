using System.Collections.Generic;
using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public interface ICategoryDao
    {
        Category GetCategory(string name);
        List<Category> GetCategories();
        Category GetCategoryByProduct(Product product);
        void AddCategory(Category category);
        void DeleteCategory(string name);
    }
}
