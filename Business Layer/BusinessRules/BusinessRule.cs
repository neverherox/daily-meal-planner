using Meal_Planner.Business_Layer;
namespace Business_Layer.BusinessRules
{
    public abstract class BusinessRule
    {
        public BusinessRule(string property)
        {
            Property = property;
        }
        public string Error { get; set; }
        public string Property { get; set; }
        public abstract bool Validate(BusinessObject businessObject);
        protected object GetPropertyValue(BusinessObject businessObject)
        {
            return businessObject.GetType().GetProperty(Property).GetValue(businessObject, null);

        }
    }
}
