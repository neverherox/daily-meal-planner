using System.Collections.Generic;
namespace Meal_Planner.Business_Layer
{
    public class MealTime : BusinessObject
    {

        public List<Product> products = new List<Product>();
        public string Name { get; set; }

    }
}
