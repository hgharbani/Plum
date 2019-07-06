using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plum.Data
{
    public class MaterialPrice
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaterialId { get; set; }

       
        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TypeMAterial MaterialTypeData { get; set; }

        public DateTime InsertTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<FoodMaterial> FoodMaterials { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public enum TypeMAterial : byte
        {
            Create = 1,
            Edit = 2,
            Delete = 3
        }
    }
}
