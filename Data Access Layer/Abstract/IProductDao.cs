using System.Collections.Generic;
using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public interface IProductDao
    {
        List<Product> GetProductsByCategory(string name);
        Product GetProduct(string name);
        void AddProduct(Product product, string categoryName);
        void DeleteProduct(string productName,string categoryName);
        void ChangeProductCharacs(DailyRation dailyRation, Product product, int value);
    }
}
