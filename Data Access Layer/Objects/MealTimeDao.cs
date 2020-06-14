using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public class MealTimeDao:IMealTimeDao
    {
        public void AddProduct(Product product, MealTime mealTime)
        {
            mealTime.products.Add(product);
        }
    }
}
