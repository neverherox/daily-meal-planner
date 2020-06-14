using Business_Layer.BusinessRules;
namespace Meal_Planner.Business_Layer
{
    public class Product : BusinessObject
    {
        public Product()
        {
            AddRule(new LengthRule(1, 20, "Name"));
            AddRule(new RangeRule(DataType.String, "А", "яя", "Name"));
            AddRule(new RangeRule(DataType.Double, "0", "100", "Fats"));
            AddRule(new RangeRule(DataType.Double, "0", "50", "Proteins"));
            AddRule(new RangeRule(DataType.Double, "0", "100", "Carbs"));
            AddRule(new RangeRule(DataType.Double, "0", "1000", "Calories"));
            AddRule(new RangeRule(DataType.Integer, "0", "100", "Grams"));
        }
        public string Name { get; set; }
        public double Fats { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
        public int Grams { get; set; }
        
        public Category Category { get; set; }
    }
}
