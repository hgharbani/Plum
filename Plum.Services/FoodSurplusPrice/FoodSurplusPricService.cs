using Plum.Data;
using Plum.Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace Plum.Services.FoodSurplusPrice
{
    public class FoodSurplusPricService : IFoodSurplusPricService
    {
        private PlumContext db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public FoodSurplusPricService(PlumContext context)
        {
            db = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public ICollection<Data.FoodSurplusPrice> GetFoodSurplusPricesByFoodId(int foodId)
        {
            return db.FoodSurplusPrices.Where(a => a.FoodId == foodId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodSurplusPrices"></param>
        /// <returns></returns>
        public PlumResult AddFoodSurplusPrice(ICollection<Data.FoodSurplusPrice> foodSurplusPrices)
        {
            PlumResult result = new PlumResult();
            foreach (Data.FoodSurplusPrice foodSurplusPrice in foodSurplusPrices)
            {
                db.FoodSurplusPrices.Add(foodSurplusPrice);
            }

            if (db.SaveChanges() > 0)
            {
                result.Message = "عملیات با موفقیت انجام شد";
                return result;
            }
            result.IsChange = false;
            result.Message = "عملیات انجام نشد";
            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodSurplusPrices"></param>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public PlumResult UpdateFoodSurplusPrice(ICollection<Data.FoodSurplusPrice> foodSurplusPrices,int foodId)
        {
            PlumResult result = new PlumResult();
            var foodpriceModels = GetFoodSurplusPricesByFoodId(foodId);
            foreach (Data.FoodSurplusPrice foodSurplusPrice in foodSurplusPrices)
            {
                var foodprice = foodpriceModels.First(a => a.AdjustKind == foodSurplusPrice.AdjustKind);
                foodprice.Price = foodSurplusPrice.Price;
            }

            if (db.SaveChanges() > 0)
            {
                result.Message = "عملیات با موفقیت انجام شد";
                return result;
            }
            result.IsChange = false;
            result.Message = "عملیات انجام نشد";
            return result;

        }
    }
}
