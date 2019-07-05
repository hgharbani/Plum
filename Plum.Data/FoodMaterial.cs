using System.ComponentModel.DataAnnotations;

namespace Plum.Data
{
    public class FoodMaterial
    {
        [Key]

        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FoodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaterialPriceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double MaterialTotalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual MaterialPrice MaterialPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Food Food { get; set; }
    }
}
