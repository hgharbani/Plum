using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Data.Contex;
using Plum.Model.Model;
using Plum.Model.Model.MAterial;

namespace Plum.Services.MaterialServices
{
   public class MaterialService:IMaterialService
    {
        private PlumContext _db;
        public MaterialService(PlumContext db)
        {
            _db = db;
        }

        private bool IsAnyMaterial(string MaterialName)
        {
            return _db.Materials.Any(a => a.MaterialName == MaterialName);
        }

        private bool IsAnyMaterial(Material Material)
        {
            return _db.Materials.Any(a => a.Id != Material.Id && a.MaterialName == Material.MaterialName);
        }

        public Material GetOne(int id)
        {
            Data.Material result = _db.Materials.AsNoTracking().Include(a => a.MaterialPrices)
                .FirstOrDefault(a => a.Id == id);
            return result;

        }

        public ICollection<Material> GetAll()
        {
         var result = _db.Materials.AsNoTracking().Include(a => a.MaterialPrices).ToList();
            return result;
        }

        public ICollection<Material> GetByParamert(string MaterialParaMert)
        {
            var result = _db.Materials.AsNoTracking().Include(a => a.MaterialPrices).Where(a => a.MaterialName.Contains(MaterialParaMert)).ToList();
            return result;
        }

        public Plum.Model.Model.PlumResult AddMaterial(Material Material)
        {
            var result = new PlumResult();
            if (IsAnyMaterial(Material.MaterialName))
            {
                result.IsChange = false;
                result.Message = "رکورد تکراری می باشد";
                return result;
            }

            _db.Entry(Material).State = EntityState.Added;
            result.Message = "رکورد با موفقیت ثبت شد";
            return result;
        }

        public PlumResult EditMaterial(Material Material)
        {
            var result = new PlumResult();
            if (IsAnyMaterial(Material))
            {
                result.IsChange = false;
                result.Message = "رکورد تکراری می باشد";
                return result;
            }

            _db.Entry(Material).State = EntityState.Modified;
            result.Message = "رکورد با موفقیت تغییر کرد";
            return result;
        }

        public PlumResult DeleteMaterial(Material Material)
        {

            var result = new PlumResult();
            try
            {
                _db.Entry(Material).State = EntityState.Deleted;

                result.IsChange = true;
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
            var result = new PlumResult();
            try
            {
                var foodMaterialModel = GetOne(materialId);

                if (foodMaterialModel == null)
                {
                    result.IsChange = false;
                    result.Message = "این شرکت حذف شده است";
                    return result;
                }

                if (foodMaterialModel.MaterialPrices.Any())
                {
                    result.IsChange = false;
                    result.Message = "قادر به حذف این کالا نمی باشد زیرا در چندین کالا اولیه در حال استفاده است";
                    return result;
                }
                result = DeleteMaterial(foodMaterialModel);
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
        /// <param name="materialModel"></param>
        /// <returns></returns>
        public List<Material> GetMaterialModel(MaterialModel materialModel)
        {
            var materialQuery = _db.Materials.AsNoTracking().Include(a => a.MaterialPrices);
            var materialPriceQuery = _db.MaterialsPrice.AsNoTracking().Include(a => a.Material).Where(a=>a.Active);
            if (materialModel.CompanyId > 0)
            {
                materialQuery = materialQuery.Where(a =>
                    a.MaterialPrices.Any(b => b.Active && b.CompanyId == materialModel.CompanyId));
            }
            if (!string.IsNullOrWhiteSpace(materialModel.MaterialName))
            {
                materialQuery = materialQuery.Where(a =>
                    a.MaterialName.Contains(materialModel.MaterialName));

            }
         
            return materialQuery.ToList();
        }
    }
}
