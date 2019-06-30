using System;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using Plum.Data;
using Plum.Data.Contex;
using Plum.Services.CompanyServices;
using Plum.Services.FoodMaterial;
using Plum.Services.FoodServices;
using Plum.Services.FoodSurplusPrice;
using Plum.Services.MaterialPriceServices;
using Plum.Services.MaterialServices;
using Plum.Services.UserServices;

namespace Plum.Services
{
    public class UnitOfWork : IDisposable
    {
        PlumContext db=new PlumContext();
        private IMaterialPriceService _materialPriceService;
        
        private IFoodService _foodService;
        private IFoodMaterialService _foodMaterialService;
        private IFoodSurplusPricService _foodSurplusPricService;
        private IUserServices _userServices;
        private ICompanyService _companyService;
        private IMaterialService _materialService;

        public IMaterialService MaterialService
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

        public ICompanyService CompanyService
        {
            get
            {
                if (_companyService == null)
                {
                    return _companyService = new CompanyService(db);
                }

                return _companyService;
            }
        }
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
        
        public IMaterialPriceService MaterialRepositories
        {
            get
            {
                if (_materialPriceService == null)
                {
                    return _materialPriceService = new MaterialPriceService(db);
                }

                return _materialPriceService;
            }
        }

        public IFoodMaterialService FoodMaterialService
        {
            get
            {
                if (_foodMaterialService == null)
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
                if (_foodSurplusPricService == null)
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
