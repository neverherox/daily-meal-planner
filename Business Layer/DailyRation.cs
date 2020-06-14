using System.Collections.Generic;
namespace Meal_Planner.Business_Layer
{
    public class DailyRation : BusinessObject
    {
        public List<MealTime> mealTimes = new List<MealTime>();
        private static DailyRation instance;
        private DailyRation()
        {
            for (int i = 0; i < 3; i++)
            {
                MealTime meal = new MealTime();
                if (i == 0)
                {
                    meal.Name = "Завтрак";
                }
                else if (i == 1)
                {
                    meal.Name = "Обед";
                }
                else
                {
                    meal.Name = "Ужин";
                }
                mealTimes.Add(meal);
            }
        }
        public static DailyRation GetInstance()
        {
            if (instance == null)
            {
                instance = new DailyRation();
            }
            return instance;
        }
    }
}
