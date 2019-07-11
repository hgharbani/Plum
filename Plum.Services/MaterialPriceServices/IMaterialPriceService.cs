using System.Collections.Generic;
using Plum.Data;
using Plum.Model.Model;

namespace Plum.Services.MaterialPriceServices
{
    public interface IMaterialPriceService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MaterialPrice GetOne(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        List<MaterialPrice> GetAll(bool active = true, int companyId = 0);

        PlumResult InsertMaterial(MaterialPrice material);
        PlumResult UpdateMaterial(MaterialPrice material, bool inHistory);
        PlumResult DeleteMaterial(MaterialPrice material);
        PlumResult DeleteMaterial(int materialId);
        IEnumerable<MaterialPrice> GetMaterials(string parameter, int companyId);


    }
}