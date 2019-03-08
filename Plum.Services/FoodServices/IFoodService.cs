using System.Collections.Generic;
using Plum.Data;

namespace Plum.Services.FoodServices
{
    public interface IFoodService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Food GetOne(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        List<Food> GetAll(bool active = true);

        bool InsertFood(Food food);
        bool UpdateFood(Food food);
        bool DeleteFood(Food food);
        bool DeleteFood(int foodId);
        IEnumerable<Food> GetFoods(string parameter);
    }
}