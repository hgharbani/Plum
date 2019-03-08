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
            return db.Materials.FirstOrDefault(a => a.Id == id);
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

        public bool UpdateMaterial(Material material)
        {
            try
            {
                Material materialModel = GetOne(material.Id);
                if (db.Materials.Any(a => a.Id != material.Id && a.MaterialName == material.MaterialName))
                {
                    return false;
                }
                if (materialModel.UnitPrice != material.UnitPrice)
                {

                    materialModel.Active = false;
                    materialModel.ParentId = materialModel.Id;
                    db.Materials.Add(materialModel);
                }
                db.Entry(material).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
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

