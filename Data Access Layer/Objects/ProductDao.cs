using System.Collections.Generic;
using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public class ProductDao: IProductDao
    {
        private DB db = DB.GetInstance();
        
        public List<Product> GetProductsByCategory(string name)
        {
            for (int i = 0; i < db.categories.Count; i++)
            {
                if (name == db.categories[i].Name)
                {
                    return db.categories[i].products;
                }
            }
            return null;
        }
        public Product GetProduct(string name)
        {
            for (int i = 0; i < db.categories.Count; i++)
            {
                for (int j = 0; j < db.categories[i].products.Count; j++)
                {
                    if (name == db.categories[i].products[j].Name)
                    {
                        return db.categories[i].products[j];
                    }
                }
            }
            return null;
        }
        public void AddProduct(Product product,string categoryName)
        {
            db.AddProduct(product,categoryName);
        }
        public void DeleteProduct(string productName, string categoryName)
        {
            db.DeleteProduct(productName, categoryName);
        }
        public void ChangeProductCharacs(DailyRation dailyRation, Product product, int value)
        {
            double calories = product.Calories;
            double grams = product.Grams;
            for (int i = 0; i < dailyRation.mealTimes.Count; i++)
            {
                for (int j = 0; j < dailyRation.mealTimes[i].products.Count; j++)
                {
                    if (dailyRation.mealTimes[i].products[j].Name == product.Name)
                    {
                        dailyRation.mealTimes[i].products[j].Grams = value;
                        dailyRation.mealTimes[i].products[j].Calories = (calories * value) / grams;
                    }
                }
            }
        }
    }
}
