using System.Collections.Generic;
using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public class DailyRationDao: IDailyRationDao
    {
        private DB db = DB.GetInstance();
        public void SaveDailyRation(DailyRation dailyRation)
        {
            db.SaveDailyRation(dailyRation);
        }
    }
}
