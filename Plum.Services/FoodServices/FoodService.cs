using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Data.Contex;
using Plum.Model.Model.Food;
using Plum.Model.Model.MAterial;

namespace Plum.Services.FoodServices
{
  public  class FoodService : IFoodService
  {

        private PlumContext db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public FoodService(PlumContext context)
        {
            db = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Food GetOne(int id)
        {
            return db.Foods.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Food> GetAll(bool active = true)
        {
            if (active)
            {
                return db.Foods.AsNoTracking().Include(a=>a.FoodMaterials).Include(a=>a.FoodSurplusPrices).Where(a => a.Active).ToList();

            }
            return db.Foods.AsNoTracking().Include(a => a.FoodMaterials).Include(a => a.FoodSurplusPrices).Where(a => a.Active == false).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<Food> GetAllByCompanyId(int companyId)
        {
           
            return db.Foods.AsNoTracking().Include(a => a.FoodMaterials).Include(a => a.FoodSurplusPrices).Where(a => a.Active == true && a.CompanyId== companyId).ToList();

        }
        public bool InsertFood(Food food)
        {
            try
            {

                if (db.Foods.Any(a => a.FoodName == food.FoodName &&  a.CompanyId == food.CompanyId && a.Active))
                {
                    return false;
                }
                db.Foods.Add(food);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateFood(Food food)
        {
            try
            {
                if (db.Foods.Any(a => a.Id != food.Id && a.FoodName == food.FoodName && a.CompanyId==food.CompanyId && a.Active))
                {
                    return false;
                }
               
                db.Entry(food).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFood(Food food)
        {
            try
            {
                db.Foods.Remove(food);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFood(int foodId)
        {
            try
            {
                Food foodModel = db.Foods.Include(a => a.FoodMaterials).Include(a => a.FoodSurplusPrices)
                    .FirstOrDefault(a => a.Id == foodId);
                if (foodModel != null)
                {
                    foreach (var foodMaterial in foodModel.FoodMaterials.ToList())
                    {
                        db.FoodMaterials.Remove(foodMaterial);

                    }

                    foreach (var foodModelFoodSurplusPrice in foodModel.FoodSurplusPrices.ToList())
                    {

                        db.FoodSurplusPrices.Remove(foodModelFoodSurplusPrice);

                    }

                    DeleteFood(foodModel);
                    return true;
                }

                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Food> GetFoods(string parameter)
        {
            return db.Foods.Where(c => c.FoodName.Contains(parameter)&& c.Active).ToList();
        }

        /// <summary>
        /// گرفتن لیست غذاها با توجه به مواد لازم نام غذا و شرکت
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="companyId"></param>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public IEnumerable<FoodDetailsModel> GetFoodsbyFoodDetails(string parameter,int companyId=0,int foodId=0)
        {
            var model= db.Foods.Where(c =>  c.Active).AsNoTracking();
            if (foodId > 0)
            {
                model = model.Where(a => a.Id == foodId);
            }

            if (companyId > 0)
            {
                model = model.Where(a => a.CompanyId == companyId);

            }

            if (parameter!="برای تعریف چند کالا از علامت - استفاده نمایید")
            {
                var materials = parameter.Trim().Replace(",", "-").Split('-');
                model = model.Where(a =>
                    a.FoodMaterials.Any(b => materials.Contains(b.MaterialPrice.Material.MaterialName)));
            }
            var result = model.Select(a => new FoodDetailsModel()
            {
                FoodId = a.Id,
                FoodName = a.FoodName,
                CompanyName = a.Company.CompanyName,
                FoodMaterials = a.FoodMaterials,
                FoodSurplusPrices = a.FoodSurplusPrices.ToList(),
                MaterialPrice = a.FoodMaterials.Any()? a.FoodMaterials.Sum(b => b.MaterialTotalPrice):0,
                FinalPrice = a.FoodSurplusPrices.Any(b => b.AdjustKind == 8) ? a.FoodSurplusPrices.Where(b => b.AdjustKind == 8).Select(b => b.Price).FirstOrDefault() : 0
            }).ToList();

            return result.ToList();
        }
        

    }
}
