using System.Collections.Generic;
using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public class CategoryDao : ICategoryDao
    {
        private DB db = DB.GetInstance();
        public Category GetCategoryByProduct(Product product)
        {
            return product.Category;
        }
        public List<Category> GetCategories()
        {
            return db.categories;
        }
        public void AddCategory(Category category)
        {
            db.AddCategory(category);
        }
        public void DeleteCategory(string name)
        {
            db.DeleteCategory(name);
        }
        public Category GetCategory(string name)
        {
            for (int i = 0; i < db.categories.Count; i++)
            {
                if (name == db.categories[i].Name)
                {
                    return db.categories[i];
                }
            }
            return null;
        }
    }
}
