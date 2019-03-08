using System;
using Plum.Data;
using Plum.Services.FoodServices;
using Plum.Services.MAterialServices;

namespace Plum.Services
{
    public class UnitOfWork : IDisposable
    {
        PlumContext db=new PlumContext();
        private IMaterialService _materialService;
        private IFoodService _foodService;

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
