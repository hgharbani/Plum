using System.Collections.Generic;
using Plum.Data;

namespace Plum.Services.MAterialServices
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
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        List<Material> GetAll(bool active=true);

        bool InsertMaterial(Material material);
        bool UpdateMaterial(Material material);
        bool DeleteMaterial(Material material);
        bool DeleteMaterial(int materialId);
        IEnumerable<Material> GetMaterials(string parameter);
        
    }
}