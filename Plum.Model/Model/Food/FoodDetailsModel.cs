using Plum.Data;
using Plum.Model.Model.MAterial;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plum.Model.Model.Food
{
    public class FoodDetailsModel
    {
        public FoodDetailsModel()
        {
            FoodMaterials = new List<FoodMaterialModel>();
            FoodSurplusPrices = new List<FoodSurplusPrice>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int FoodId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("نام غذا")] public string FoodName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("قیمت مواد لازم")] public double MaterialPrice { get; set; }
        [DisplayName("قیمت نهایی")] public double FinalPrice { get; set; }

        [DisplayName(" تعداد هر پرس")]
        public int Quantity { get; set; }


        public List<FoodMaterialModel> FoodMaterials { get; set; }
        public List<FoodSurplusPrice> FoodSurplusPrices { get; set; }
    }
}
