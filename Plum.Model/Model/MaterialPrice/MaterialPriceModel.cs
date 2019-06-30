using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Model.Model.MaterialPrice
{
  public  class MaterialPriceModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int MateriaPriceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MateriaName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MaterialCompany { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime InsertTime { get; set; }

    }
}
