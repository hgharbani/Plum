using System.Collections.Generic;

namespace Plum.Data
{
    public class Food
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FoodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual  ICollection<FoodMaterial> FoodMaterials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<FoodSurplusPrice> FoodSurplusPrices { get; set; }

    }
}
