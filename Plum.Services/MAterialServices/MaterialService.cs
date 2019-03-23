using Plum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Plum.Services.MAterialServices
{
    public class MaterialService : IMaterialService
    {
        private PlumContext db;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MaterialService(PlumContext context)
        {
            db = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Material GetOne(int id)
        {
            return db.Materials.AsQueryable().Include(a=>a.FoodMaterials).FirstOrDefault(a => a.Id == id );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Material> GetAll(bool active = true)
        {
            if (active)
            {
                return db.Materials.Where(a => a.Active).ToList();

            }
            return db.Materials.Where(a => a.Active == false).ToList();

        }

        public bool InsertMaterial(Material material)
        {
            try
            {

                if (db.Materials.Any(a => a.MaterialName == material.MaterialName && a.Active==material.Active))
                {
                    return false;
                }
                db.Materials.Add(material);
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
        public bool UpdateMaterial(Material material, bool inHistory)
        {
            try
            {
                Material materialModel = GetOne(material.Id);
                if (db.Materials.Any(a => a.Id != material.Id && a.MaterialName == material.MaterialName&&a.Active))
                {
                    return false;
                }
                if (materialModel.UnitPrice != material.UnitPrice && inHistory)
                {
                    var oldMaterial=new Material()
                    {
                        MaterialName = materialModel.MaterialName,
                        Active = false,
                        ParentId = materialModel.Id,
                        UnitPrice = materialModel.UnitPrice,
                        InsertTime = materialModel.InsertTime,
                        MaterialTypeData = materialModel.MaterialTypeData
                    };
                 
                    db.Materials.Add(oldMaterial);
                }

                materialModel.MaterialName = material.MaterialName;
                materialModel.UnitPrice = material.UnitPrice;
                materialModel.InsertTime = material.InsertTime;
                
                foreach (var item in materialModel.FoodMaterials.ToList())
                {
                    item.UnitPrice = material.UnitPrice;

                    var unitPrice = material.UnitPrice / 1000;
                    var quantity = item.Quantity;
                    var totalPrice = unitPrice * quantity;
                    item.MaterialTotalPrice = totalPrice;
                    db.Entry(item).State = EntityState.Modified;
                }

              
               
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                var x = e;
                return false;
            }
        }

        public bool DeleteMaterial(Material material)
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

        public IEnumerable<Material> GetMaterials(string parameter)
        {
            return db.Materials.Where(c => c.MaterialName.Contains(parameter) || c.UnitPrice.ToString().Contains(parameter)&& c.Active).ToList();
        }
    }
}

