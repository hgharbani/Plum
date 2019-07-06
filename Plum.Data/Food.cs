using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plum.Data
{
    public class Food
    {
        /// <summary>
        /// 
        /// </summary>
       [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FoodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

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
