using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Model.Model;
using Plum.Model.Model.MAterial;

namespace Plum.Services.MaterialServices
{
  public interface IMaterialService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Material GetOne(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<Material> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaterialParaMert"></param>
        /// <returns></returns>
        ICollection<Material> GetByParamert(string MaterialParaMert);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Material"></param>
        /// <returns></returns>
        PlumResult AddMaterial(Material Material);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Material"></param>
        /// <returns></returns>
        PlumResult EditMaterial(Material Material);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Material"></param>
        /// <returns></returns>
        PlumResult DeleteMaterial(Data.Material Material);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Plum.Model.Model.PlumResult DeleteMaterial(int materialId);

        List<Material> GetMaterialModel(MaterialModel materialModel);
    }
}
