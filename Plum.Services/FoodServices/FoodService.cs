using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;

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

        public bool InsertFood(Food food)
        {
            try
            {

                if (db.Foods.Any(a => a.FoodName == food.FoodName && a.Active == food.Active))
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
                if (db.Foods.Any(a => a.Id != food.Id && a.FoodName == food.FoodName))
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
                Food foodModel = db.Foods.AsQueryable().Include(a => a.FoodMaterials).Include(a => a.FoodSurplusPrices)
                    .FirstOrDefault(a => a.Id == foodId);
                foreach (var foodMaterial in foodModel.FoodMaterials)
                {
                    db.FoodMaterials.Remove(foodMaterial);

                }

                foreach (var foodModelFoodSurplusPrice in foodModel.FoodSurplusPrices)
                {

                    db.FoodSurplusPrices.Remove(foodModelFoodSurplusPrice);

                }
                DeleteFood(foodModel);
                return true;
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

      
    }
}
