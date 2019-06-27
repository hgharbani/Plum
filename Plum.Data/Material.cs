using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Data
{
    /// <summary>
    /// 
    /// </summary>
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
        public virtual  ICollection<MaterialPrice> MaterialPrices { get; set; }
    }
}
