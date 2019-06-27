using Plum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Plum.Model.Model;

namespace Plum.Services.FoodMaterial
{
    /// <summary>
    /// 
    /// </summary>
    public class FoodMaterialService : IFoodMaterialService
    {
        private PlumContext _db;
        public FoodMaterialService(PlumContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.FoodMaterial GetOne(int id)
        {
            Data.FoodMaterial result = _db.FoodMaterials.AsNoTracking().Include(a => a.MaterialPrice).Include(a => a.Food).FirstOrDefault(a => a.Id == id);
            return result;
        }

        /// <summary>
        /// جستجو مواد لازم بر اساس کد غذا
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public ICollection<Data.FoodMaterial> GetOneByFoodId(int foodId)
        {
            var result = _db.FoodMaterials.AsNoTracking().Include(a => a.MaterialPrice).Include(a => a.Food).Where(a => a.FoodId == foodId).ToList();
            return result;
        }

        /// <summary>
        /// گرفتن یک مواد لازم بر اساس مشخصه مواد لازم
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public Data.FoodMaterial GetOneByMaterialId(int foodId,int materialId)
        {
            Data.FoodMaterial result = _db.FoodMaterials.AsNoTracking().Include(a => a.MaterialPrice).Include(a => a.Food).FirstOrDefault(a =>a.FoodId==foodId&& a.MaterialId == materialId);
            return result;
        }

        /// <summary>
        /// گرفتن لیست غذاهایی که از یک مواد لازم مشترک استفاده شده است
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public ICollection<Data.FoodMaterial> GetFoodMaterialsByMaterialId(int materialId)
        {
            var result = _db.FoodMaterials.AsNoTracking().Include(a => a.MaterialPrice).Include(a => a.Food).Where(a =>  a.MaterialId == materialId).ToList();
            return result;
        }


        /// <summary>
        /// گرفتن لیست غذاهایی که از چند نمواد مکشترک تشکیل  شده است
        /// </summary>
        /// <param name="materialIds"></param>
        /// <returns></returns>
        public ICollection<Data.FoodMaterial> GetFoodMaterialsByMaterialId(int[] materialIds)
        {
            var result = _db.FoodMaterials.AsNoTracking().Include(a => a.MaterialPrice).Include(a => a.Food).Where(a => materialIds.Contains(a.MaterialId)).ToList();
            return result;
        }

        public ICollection<Data.FoodMaterial> GetAll()
        {
            List<Data.FoodMaterial> result = _db.FoodMaterials.AsNoTracking().Include(a => a.MaterialPrice).Include(a => a.Food).Where(a => a.Active).ToList();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public bool IsAnyMaterial(int foodId, int materialId)
        {
            return _db.FoodMaterials.Any(a => a.FoodId == foodId && a.MaterialId == materialId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public bool IsAnyMaterial(int Id, int foodId, int materialId)
        {
            return _db.FoodMaterials.Any(a => a.Id != Id && a.FoodId == foodId && a.MaterialId == materialId);
        }

        public bool AddFoodMaterial(Data.FoodMaterial foodMaterial)
        {
            try
            {
                if (IsAnyMaterial(foodMaterial.FoodId, foodMaterial.MaterialId) == true)
                {
                    return false;
                }
                _db.FoodMaterials.Add(foodMaterial);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFoodMaterial(Data.FoodMaterial foodMaterial)
        {
            try
            {
                if (IsAnyMaterial(foodMaterial.Id,foodMaterial.FoodId, foodMaterial.MaterialId) == true)
                {
                    return false;
                }
                _db.Entry(foodMaterial).State = EntityState.Modified;
              
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PlumResult DeleteFoodMAterial(Data.FoodMaterial foodMaterial)
        {
            var result = new PlumResult();
            try
            {
                
                _db.Entry(foodMaterial).State=EntityState.Deleted;
                if (_db.SaveChanges() > 0)
                {
                    result.IsChange = true;
                    result.Message = "کالا با موفقیت حذف شد";
                    return result;
                }
               
            }
            catch (Exception e)
            {
                result.IsChange = false;
                result.Message =e.Message;

                return result;
            }
            result.IsChange = false;
            result.Message = "خطایی رخ داده است";

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodMaterialId"></param>
        /// <returns></returns>
        public PlumResult DeleteFood(int foodMaterialId)
        {
            var result=new PlumResult();
            try
            {
                var foodMaterialModel = GetOne(foodMaterialId);
                if (foodMaterialModel == null)
                {
                    result.Message = "این کالا حذف شده است";
                }
               result= DeleteFoodMAterial(foodMaterialModel);
                return result;
            }
            catch (Exception)
            {
                result.IsChange = false;
                result.Message = "خطایی رخ داده است";

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IEnumerable<Data.FoodMaterial> GetMaterialFoods(int foodId, string parameter)
        {
            
            return _db.FoodMaterials.AsNoTracking().Include(a=>a.MaterialPrice).Where(c => c.MaterialPrice.Material.MaterialName.Contains(parameter) || c.MaterialTotalPrice==double.Parse(parameter) || c.UnitPrice == double.Parse(parameter)).ToList();
        }



    }
}
