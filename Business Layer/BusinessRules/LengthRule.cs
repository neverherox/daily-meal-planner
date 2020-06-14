using Meal_Planner.Business_Layer;
namespace Business_Layer.BusinessRules
{
    class LengthRule : BusinessRule
    {
        private int min, max;
        public LengthRule(int min, int max,string property):base(property)
        {
            this.min = min;
            this.max = max;
        }
        public override bool Validate(BusinessObject businessObject)
        {
            int length = GetPropertyValue(businessObject).ToString().Length;
            if (length >= min && length <= max)
            {
                return true;
            }
            Error = "Length should be between " + min + " and " + max + " characters long.";
            return false;
        }
    }
}
