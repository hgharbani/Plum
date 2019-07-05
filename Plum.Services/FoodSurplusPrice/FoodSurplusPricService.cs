using Plum.Data;
using Plum.Model.Model;
using System.Collections.Generic;
using System.Linq;
using Plum.Data.Contex;

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
        /// <param name="companyId"></param>
        /// <returns></returns>
        public ICollection<Data.FoodSurplusPrice> GetFoodSurplusPricesByCompany(int companyId)
        {
            return db.FoodSurplusPrices.Where(a => a.Food.CompanyId == companyId).ToList();
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

        /// <summary>
        /// محاسبه هزینه های مازاد غذا با توجه به مقدار سفارش و قیمت نهایی
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="sumTotalMaterialPrice"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public double CalculatorFinalPrice(int foodId,double sumTotalMaterialPrice,int quantity)
        {
            var sum = 0.0;
            var foodpriceModels = GetFoodSurplusPricesByFoodId(foodId);
            if (foodpriceModels.Any())
            {
                double totalPeice = sumTotalMaterialPrice;
                double personNumeric = foodpriceModels.First(a => a.AdjustKind == 1).Price*quantity;
                double chashniNumeric = foodpriceModels.First(a => a.AdjustKind == 2).Price * quantity;
                double cleanNumeric = foodpriceModels.First(a => a.AdjustKind == 3).Price * quantity;
                double boxNumeric = foodpriceModels.First(a => a.AdjustKind == 4).Price * quantity;
                double parentNumeric = foodpriceModels.First(a => a.AdjustKind == 5).Price * quantity;
                double bimehNumeric = foodpriceModels.First(a => a.AdjustKind == 6).Price;
                double taxNumeric = foodpriceModels.First(a => a.AdjustKind == 7).Price;

                sum = sum + (totalPeice + personNumeric + chashniNumeric + cleanNumeric + boxNumeric + parentNumeric);
                var SumBimeh = 0.0;//حاصل جمع مقادیر * مقدار بیمه
                if (bimehNumeric > 0)
                {
                    SumBimeh = sum * bimehNumeric;

                }
                //مجموعه هزینه ها * مقدار مالیات
                var maliyat = 0.0;
                if (taxNumeric > 0)
                {
                    maliyat = sum * taxNumeric;

                }

                sum = sum + maliyat + SumBimeh;
            }
            
            return sum;
        }
    }
}
