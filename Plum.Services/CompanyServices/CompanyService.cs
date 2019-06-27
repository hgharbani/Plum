using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Model.Model;

namespace Plum.Services.CompanyServices
{
   public class CompanyService:ICompanyService
    {
        private PlumContext _db;
        public CompanyService(PlumContext db)
        {
            _db = db;
        }

        private bool IsAnyCompany(string companyName)
        {
            return _db.Companies.Any(a => a.CompanyName == companyName);
        }

        private bool IsAnyCompany(Company company)
        {
            return _db.Companies.Any(a =>a.CompanyId!=company.CompanyId&& a.CompanyName == company.CompanyName);
        }

        public Company GetOne(int id)
        {
            Data.Company result = _db.Companies.AsNoTracking().Include(a=>a.MaterialsPrices).Include(a => a.Foods).FirstOrDefault(a => a.CompanyId == id);
            return result;
        }

        public ICollection<Company> GetAll()
        {
            var result = _db.Companies.AsNoTracking().Include(a => a.MaterialsPrices).Include(a=>a.Foods).ToList();
            return result;
        }

        public ICollection<Company> GetByParamert(string CompanyParaMert)
        {
            var result = _db.Companies.AsNoTracking().Include(a => a.MaterialsPrices).Include(a => a.Foods).Where(a=>a.CompanyName.Contains(CompanyParaMert)).ToList();
            return result;
        }

        public PlumResult AddCompany(Company company)
        {
            var result=new PlumResult();
            if (IsAnyCompany(company.CompanyName))
            {
                result.IsChange = false;
                result.Message = "رکورد تکراری می باشد";
                return result;
            }

            _db.Entry(company).State = EntityState.Added;
            result.Message = "رکورد با موفقیت ثبت شد";
            return result;
        }

        public PlumResult EditCompany(Company company)
        {
            var result = new PlumResult();
            if (IsAnyCompany(company))
            {
                result.IsChange = false;
                result.Message = "رکورد تکراری می باشد";
                return result;
            }

            _db.Entry(company).State = EntityState.Modified;
            result.Message = "رکورد با موفقیت تغییر کرد";
            return result;
        }

        public PlumResult DeleteCompany(Company company)
        {
            var result = new PlumResult();
            try
            {

                _db.Entry(company).State = EntityState.Deleted;
               
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

        public PlumResult DeleteCompany(int companyId)
        {
            var result = new PlumResult();
            try
            {
                var foodMaterialModel = GetOne(companyId);
                if (foodMaterialModel == null)
                {
                    result.Message = "این شرکت حذف شده است";
                }
                result = DeleteCompany(foodMaterialModel);
                return result;
            }
            catch (Exception)
            {
                result.IsChange = false;
                result.Message = "خطایی رخ داده است";

                return result;
            }
        }

    }
}
