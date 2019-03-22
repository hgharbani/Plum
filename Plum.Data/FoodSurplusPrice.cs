using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Data
{
   public  class FoodSurplusPrice
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FoodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  string CostTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AdjustKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Food Food { get; set; }
    }
}
