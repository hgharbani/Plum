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
        /// <param name="companyId"></param>
        /// <returns></returns>
        ICollection<Data.FoodSurplusPrice> GetFoodSurplusPricesByCompany(int companyId);
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

        /// <summary>
        /// محاسبه مقدار هزینه مازاد با توجه به تعداد درخواست غذا
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="sumTotalMaterialPrice"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        double CalculatorFinalPrice(int foodId, double sumTotalMaterialPrice, int quantity);
    }
}