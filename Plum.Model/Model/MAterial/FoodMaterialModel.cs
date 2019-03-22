using System.ComponentModel;

namespace Plum.Model.Model.MAterial
{
    public class FoodMaterialModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("مواد لازم")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("مقدار لازم")]
        public double Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("قیمت هر کیلو")]
        public double Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("قیمت نهایی")]
        public double TotalPrice { get; set; }
        
    }
}
