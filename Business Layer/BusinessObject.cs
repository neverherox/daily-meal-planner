using Business_Layer.BusinessRules;
using System.Collections.Generic;
namespace Meal_Planner.Business_Layer
{
    public abstract class BusinessObject
    {
        List<BusinessRule> rules = new List<BusinessRule>();
        List<string> errors = new List<string>();
        public List<string> Errors
        {
            get { return errors; }
        }
        public void AddRule(BusinessRule rule)
        {
            rules.Add(rule);
        }
        public bool IsValid()
        {
            bool valid = true;
            errors.Clear();

            foreach (var rule in rules)
            {
                if (!rule.Validate(this))
                {
                    valid = false;
                    errors.Add(rule.Error);
                }
            }
            return valid;
        }
    }
}
