using System;
using Plum.Data;
using Plum.Services.FoodMaterial;
using Plum.Services.FoodServices;
using Plum.Services.FoodSurplusPrice;
using Plum.Services.MAterialServices;

namespace Plum.Services
{
    public class UnitOfWork : IDisposable
    {
        PlumContext db=new PlumContext();
        private IMaterialService _materialService;
        private IFoodService _foodService;
        private IFoodMaterialService _foodMaterialService;
        private IFoodSurplusPricService _foodSurplusPricService;
        public IFoodService FoodService
        {
            get
            {
                if (_foodService == null)
                {
                    return _foodService = new FoodService(db);
                }

                return _foodService;
            }
        }
        
        public IMaterialService MaterialRepositories
        {
            get
            {
                if (_materialService == null)
                {
                    return _materialService = new MaterialService(db);
                }

                return _materialService;
            }
        }

        public IFoodMaterialService FoodMaterialService
        {
            get
            {
                if (_materialService == null)
                {
                    return _foodMaterialService = new FoodMaterialService(db);
                }

                return _foodMaterialService;
            }
        }

        public IFoodSurplusPricService FoodSurplusPricService
        {
            get
            {
                if (_materialService == null)
                {
                    return _foodSurplusPricService = new FoodSurplusPricService(db);
                }

                return _foodSurplusPricService;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();

        }
    }
}
