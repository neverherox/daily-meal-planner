using Meal_Planner.Business_Layer;
namespace Business_Layer.BusinessRules
{
    class RangeRule : BusinessRule
    {
        private DataType type;
        private object min, max;
        public RangeRule(DataType type, object min, object max, string property) : base(property)
        {
            this.type = type;
            this.min = min;
            this.max = max;
        }
        public override bool Validate(BusinessObject businessObject)
        {
            string value = GetPropertyValue(businessObject).ToString();
            switch(type)
            {
                case DataType.Integer:
                    int imin = int.Parse(min.ToString());
                    int imax = int.Parse(max.ToString());
                    int ival = int.Parse(value);
                    if (ival >= imin && ival <= imax)
                    {
                        return true;
                    }
                    break;
                case DataType.Double:
                    double dmin = double.Parse(min.ToString());
                    double dmax = double.Parse(max.ToString());
                    double dval = double.Parse(value);
                    if (dval >= dmin && dval <= dmax)
                    {
                        return true;
                    }
                    break;
                case DataType.String:
                    string smin = min.ToString();
                    string smax = max.ToString();

                    int result1 = string.Compare(value, smin, true);
                    int result2 = string.Compare(value, smax, true);

                    if (result1 >= 0 && result2 <= 0)
                    {
                        return true;
                    }
                    break;
            }
            Error = Property + " value should be between " + min + " and " + max;
            return false;
        }
    }
}
