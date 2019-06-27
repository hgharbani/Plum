using Plum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Plum.Services.MaterialPriceServices
{
    public class MaterialPriceService : IMaterialPriceService
    {
        private PlumContext db;
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MaterialPriceService(PlumContext context)
        {
            db = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaterialPrice GetOne(int id)
        {
            return db.MaterialsPrice.AsQueryable().Include(a=>a.FoodMaterials).Include(a=>a.Material).FirstOrDefault(a => a.Id == id );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<MaterialPrice> GetAll(bool active = true)
        {
            if (active)
            {
                return db.MaterialsPrice.Where(a => a.Active).ToList();

            }
            return db.MaterialsPrice.ToList();

        }

        public bool InsertMaterial(MaterialPrice material)
        {
            try
            {

                if (db.MaterialsPrice.Any(a => a.MaterialId == material.MaterialId && a.Active==material.Active))
                {
                    return false;
                }
                db.MaterialsPrice.Add(material);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="inHistory"></param>
        /// <returns></returns>
        public bool UpdateMaterial(MaterialPrice material, bool inHistory)
        {
            try
            {
                MaterialPrice materialModel = GetOne(material.Id);
                if (db.MaterialsPrice.Any(a => a.Id != material.Id && a.MaterialId == material.MaterialId&& a.Active))
                {
                    return false;
                }
                if (materialModel.UnitPrice != material.UnitPrice && inHistory)
                {
                    var oldMaterial=new MaterialPrice()
                    {
                        MaterialId = materialModel.MaterialId,
                        Active = false,
                        ParentId = materialModel.Id,
                        UnitPrice = materialModel.UnitPrice,
                        InsertTime = materialModel.InsertTime,
                        MaterialTypeData = materialModel.MaterialTypeData
                    };
                 
                    db.MaterialsPrice.Add(oldMaterial);
                }

                materialModel.MaterialId = material.MaterialId;
                materialModel.UnitPrice = material.UnitPrice;
                materialModel.InsertTime = material.InsertTime;
                
                //تغییر قیمت یک در لیست مواد اولیه غذاها
                foreach (var item in materialModel.FoodMaterials.ToList())
                {
                    item.UnitPrice = material.UnitPrice;

                    var unitPrice = material.UnitPrice / 1000;
                    var quantity = item.Quantity;
                    var totalPrice = unitPrice * quantity;
                    item.MaterialTotalPrice = totalPrice;
                    db.Entry(item).State = EntityState.Modified;

                    var foodId = item.FoodId;
                    //پس از تغییر قیمت کالا قیمت ناهایی کالا نیز تغییر میکند
                    if (item.Food.FoodSurplusPrices.Any())
                    {
                       
                        var foodPRice = item.Food.FoodSurplusPrices.FirstOrDefault(a => a.AdjustKind == 8);
                        var sumfoodMAterialPrice = 0.0;
                        using (var unit = new UnitOfWork())
                        {
                            var foodDetails = unit.FoodService.GetOne(foodId);
                            foreach (var foodMaterial in foodDetails.FoodMaterials)
                            {
                                if (foodMaterial.Id == item.Id)
                                {
                                    sumfoodMAterialPrice = sumfoodMAterialPrice + totalPrice;
                                }
                                else
                                {
                                    sumfoodMAterialPrice = sumfoodMAterialPrice + foodMaterial.MaterialTotalPrice;
                                }
                               
                            }
                           var sumFoodPrice = unit.FoodSurplusPricService.CalculatorFinalPrice(foodId, sumfoodMAterialPrice, 1);
                            if (foodPRice != null) foodPRice.Price = sumFoodPrice;
                        
                        }
                        db.Entry(foodPRice).State = EntityState.Modified;
                    }
                        
                }

                return true;
            }
            catch (Exception e)
            {
                var x = e;
                return false;
            }
        }

        public bool DeleteMaterial(MaterialPrice material)
        {
            try
            {
                db.Entry(material).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMaterial(int materialId)
        {
            var material = GetOne(materialId);
            if (material.FoodMaterials.Any())
            {
                return false;
            }
            try
            {
                DeleteMaterial(material);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MaterialPrice> GetMaterials(string parameter)
        {
            return db.MaterialsPrice.Where(c => c.Material.MaterialName.Contains(parameter) || c.UnitPrice.ToString().Contains(parameter)&& c.Active).ToList();
        }
    }
}

