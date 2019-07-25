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
            FoodMaterials = new List<FoodMaterial>();
            FoodMaterialsModel = new List<FoodMaterialModel>();
            FoodSurplusPrices = new List<FoodSurplusPrice>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int FoodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("نام غذا")]
        public string FoodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("نام شرکت")]
        public string CompanyName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("قیمت مواد لازم")]
        public double MaterialPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("قیمت نهایی")]
        public double FinalPrice { get; set; }

        [DisplayName(" تعداد هر پرس")]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<FoodMaterial> FoodMaterials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<FoodMaterialModel> FoodMaterialsModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<FoodSurplusPrice> FoodSurplusPrices { get; set; }
    }
}
