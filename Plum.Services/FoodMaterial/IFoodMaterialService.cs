using System.Collections.Generic;
using Plum.Model.Model;

namespace Plum.Services.FoodMaterial
{
    public interface IFoodMaterialService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Data.FoodMaterial GetOne(int id);

        /// <summary>
        /// جستجو مواد لازم بر اساس کد غذا
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        ICollection<Data.FoodMaterial> GetOneByFoodId(int foodId);

        /// <summary>
        /// گرفتن یک مواد لازم بر اساس مشخصه مواد لازم
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Data.FoodMaterial GetOneByMaterialId(int foodId,int materialId);

        /// <summary>
        /// گرفتن لیست غذاهایی که در آن از یک مواد استفغاده شده است
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        ICollection<Data.FoodMaterial> GetFoodMaterialsByMaterialId(int materialId);

        ICollection<Data.FoodMaterial> GetFoodMaterialsByMaterialId(int[] materialIds);

        ICollection<Data.FoodMaterial> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        bool IsAnyMaterial(int foodId, int materialId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        bool IsAnyMaterial(int Id, int foodId, int materialId);

        bool AddFoodMaterial(Data.FoodMaterial foodMaterial);
        bool UpdateFoodMaterial(Data.FoodMaterial foodMaterial);
        PlumResult DeleteFoodMAterial(Data.FoodMaterial foodMaterial);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodMaterialId"></param>
        /// <returns></returns>
        PlumResult DeleteFood(int foodMaterialId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IEnumerable<Data.FoodMaterial> GetMaterialFoods(int foodId, string parameter);
    }
}