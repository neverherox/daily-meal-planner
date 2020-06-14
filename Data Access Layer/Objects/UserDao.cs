using Meal_Planner.Business_Layer;
namespace Meal_Planner.Data_Access_Layer
{
    public class UserDao: IUserDao
    {
        public double DailyColoriesRate(User user)
        {
            double BMR = 447.593 + (9.247 * user.Weight) + (3.098 * user.Height) - (4.330 * user.Age);
            if (user.Activity == "Низкая")
            {
                return BMR * 1.2;
            }
            if (user.Activity == "Умеренная")
            {
                return BMR * 1.375;
            }
            if (user.Activity == "Средняя")
            {
                return BMR * 1.55;
            }
            return BMR * 1.725;
        }
    }
}
