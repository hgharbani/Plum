using Plum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Plum.Data.Contex;
using Plum.Model.Model;

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
            return db.MaterialsPrice.AsQueryable().Include(a => a.FoodMaterials).Include(a => a.Material).FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<MaterialPrice> GetAll(bool active = true, int companyId = 0)
        {
            var result = db.MaterialsPrice.AsNoTracking().Include(a => a.Company).Include(a => a.Material).AsNoTracking();
            if (active)
            {
                result = result.Where(a => a.Active == active);
            }
            if (companyId > 0)
            {
                result = result.Where(a => a.CompanyId == companyId);
            }
            return result.ToList();

        }

        public PlumResult InsertMaterial(MaterialPrice material)
        {
            var result = new PlumResult();
            try
            {

                if (db.MaterialsPrice.Any(a => a.MaterialId == material.MaterialId && a.CompanyId == material.CompanyId && a.Active))
                {
                    result.IsChange=false;
                    result.Message = "کالای وارد شده قبلا قیمت گذاری شده است";
                    return result;
                }
                db.MaterialsPrice.Add(material);
                result.Message = "عملیات با موفقیت انجام شد";
                return result;
            }
            catch (Exception e)
            {
                result.IsChange = false;

                result.Message = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="inHistory"></param>
        /// <returns></returns>
        public PlumResult UpdateMaterial(MaterialPrice material, bool inHistory)
        {
            var result = new PlumResult();

            try
            {
                MaterialPrice materialModel = GetOne(material.Id);
                if (db.MaterialsPrice.Any(a => a.Id != material.Id && a.MaterialId == material.MaterialId && a.CompanyId == material.CompanyId && a.Active))
                {
                    result.IsChange = false;
                    result.Message = "کالای وارد شده قبلا قیمت گذاری شده است";
                    return result;
                }
                if (materialModel.UnitPrice != material.UnitPrice && inHistory)
                {
                    var oldMaterial = new MaterialPrice()
                    {
                        MaterialId = materialModel.MaterialId,
                        Active = false,
                        CompanyId = materialModel.CompanyId,
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

                result.Message = "عملیات با موفقیت انجام شد";
                return result;
            }
            catch (Exception e)
            {
                result.IsChange = false;

                result.Message =e.Message;
                return result;
            }
        }

        public PlumResult DeleteMaterial(MaterialPrice material)
        {
            var result = new PlumResult();
            try
            {
                db.Entry(material).State = EntityState.Deleted;
                result.Message = "کالا با موفقیت حذف شد";
                return result;
            }
            catch (Exception e)
            {
                result.IsChange = false;
                result.Message = e.Message;
                return result;
            }
        }

        public PlumResult DeleteMaterial(int materialId)
        {
            var result=new PlumResult();
            var material = GetOne(materialId);
            if (material.FoodMaterials.Any())
            {
                result.IsChange = false;
                result.Message = "قادر به حذف این کالا نمی باشید زیرا در بعضی از غذاها استفاده شده است";
                return result;
            }
            try
            {
              result=  DeleteMaterial(material);
                return result;
            }
            catch (Exception e)
            {
                result.IsChange = false;
                result.Message = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public IEnumerable<MaterialPrice> GetMaterials(string parameter, int companyId = 0)
        {
            var result = db.MaterialsPrice.AsNoTracking().Include(a => a.Material).Where(c => c.Active);
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                result = db.MaterialsPrice.AsNoTracking().Include(a => a.Material).Where(c => c.Material.MaterialName.Contains(parameter) || c.UnitPrice.ToString().Contains(parameter));

            }

            if (companyId > 0)
            {
                result = result.Where(a => a.CompanyId == companyId);
            }

            return result.ToList();
        }
    }
}

