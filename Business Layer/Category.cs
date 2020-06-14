using Business_Layer.BusinessRules;
using System;
using System.Collections.Generic;
namespace Meal_Planner.Business_Layer
{
    public class Category : BusinessObject
    {
        public Category()
        {
            AddRule(new LengthRule(1, 20, "Name"));
            AddRule(new RangeRule(DataType.String, "А", "яя", "Name"));
        }
        public List<Product> products = new List<Product>();
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
