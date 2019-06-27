using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Data
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<MaterialPrice> MaterialsPrices { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
