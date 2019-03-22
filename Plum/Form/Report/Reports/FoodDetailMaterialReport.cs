using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Model.Model.Food;
using Plum.Model.Model.MAterial;
using Plum.Services;

namespace Plum.Form.Report.Reports
{
  public  class FoodDetailMaterialReport:Telerik.Reporting.Report
    {
        public FoodDetailMaterialReport()
        {

        }
        /// <summary>
        /// مشخصات یک پرس غذا
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public Telerik.Reporting.Report GetFoodDetail(int foodId)
        {
            using (var db = new UnitOfWork())
            {
                var food = db.FoodService.GetOne(foodId);
               
                    var model = new FoodDetailsModel()
                    {
                        FoodId = food.Id,
                        FoodName = food.FoodName,
                        FoodMaterials = new List<FoodMaterialModel>(),
                        FoodSurplusPrices = food.FoodSurplusPrices
                    };
                  
                

                foreach (var matetrial in food.FoodMaterials)
                {
                    var foodMaterialModel = new FoodMaterialModel()
                    {
                        MaterialName = matetrial.Material.MaterialName,
                        Price = matetrial.UnitPrice,
                        Quantity = matetrial.Quantity,
                        TotalPrice = matetrial.MaterialTotalPrice,
                       
                    };
                    model.FoodMaterials.Add(foodMaterialModel);
                }
                return new RptFoodDetails(model);
            }
        
        }
    }
}
