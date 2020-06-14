using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public interface IMealTimeDao
    {
        void AddProduct(Product product, MealTime mealTime);
    }
}
