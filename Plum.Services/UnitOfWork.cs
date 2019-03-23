using System;
using Plum.Data;
using Plum.Services.FoodMaterial;
using Plum.Services.FoodServices;
using Plum.Services.FoodSurplusPrice;
using Plum.Services.MAterialServices;
using Plum.Services.UserServices;

namespace Plum.Services
{
    public class UnitOfWork : IDisposable
    {
        PlumContext db=new PlumContext();
        private IMaterialService _materialService;
        private IFoodService _foodService;
        private IFoodMaterialService _foodMaterialService;
        private IFoodSurplusPricService _foodSurplusPricService;
        private IUserServices _userServices;
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
        public IUserServices UserServices
        {
            get
            {
                if (_userServices==null)
                {
                    return _userServices = new UserServices.UserServices(db);

                }

                return _userServices;
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
