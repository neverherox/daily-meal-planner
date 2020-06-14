using Meal_Planner.Business_Layer;
using System.Collections.Generic;
namespace Meal_Planner.Data_Access_Layer
{
    public interface IDailyRationDao
    {
        void SaveDailyRation(DailyRation dailyRation);
    }
}
