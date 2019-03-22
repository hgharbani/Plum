using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Model.Model.MAterial;

namespace Plum.Model.Model.Food
{
  public  class FoodDetailsModel
    {
        public FoodDetailsModel()
        {
            FoodMaterials=new List<FoodMaterialModel>();
            FoodSurplusPrices=new List<FoodSurplusPrice>();
        }

        public int FoodId { get; set; }
        public string FoodName { get; set; }

        public ICollection<FoodMaterialModel> FoodMaterials { get; set; }
        public ICollection<FoodSurplusPrice> FoodSurplusPrices { get; set; }
    }
}
