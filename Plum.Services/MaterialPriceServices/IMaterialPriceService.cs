using System.Collections.Generic;
using Plum.Data;

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

        bool InsertMaterial(MaterialPrice material);
        bool UpdateMaterial(MaterialPrice material, bool inHistory);
        bool DeleteMaterial(MaterialPrice material);
        bool DeleteMaterial(int materialId);
        IEnumerable<MaterialPrice> GetMaterials(string parameter, int companyId);


    }
}