using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plum.Data
{
    public class Material
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MaterialName { get; set; }
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
        public bool Active { get; set; }


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
