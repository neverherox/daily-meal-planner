using Business_Layer.BusinessRules;
namespace Meal_Planner.Business_Layer
{
    public class User : BusinessObject
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Activity { get; set; }
    }
}
