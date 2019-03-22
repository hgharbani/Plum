using System.Collections.Generic;
using Plum.Model.Model;

namespace Plum.Services.FoodSurplusPrice
{
    public interface IFoodSurplusPricService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        ICollection<Data.FoodSurplusPrice> GetFoodSurplusPricesByFoodId(int foodId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodSurplusPrices"></param>
        /// <returns></returns>
        PlumResult AddFoodSurplusPrice(ICollection<Data.FoodSurplusPrice> foodSurplusPrices);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodSurplusPrices"></param>
        /// <param name="foodId"></param>
        /// <returns></returns>
        PlumResult UpdateFoodSurplusPrice(ICollection<Data.FoodSurplusPrice> foodSurplusPrices,int foodId);
    }
}