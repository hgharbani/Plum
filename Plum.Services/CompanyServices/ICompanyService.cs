using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Model.Model;

namespace Plum.Services.CompanyServices
{
    public interface ICompanyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Company GetOne(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<Company> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyParaMert"></param>
        /// <returns></returns>
        ICollection<Company> GetByParamert(string CompanyParaMert);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        PlumResult AddCompany(Company company);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        PlumResult EditCompany(Company company);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        PlumResult DeleteCompany(Data.Company company);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        PlumResult DeleteCompany(int companyId);


    }
}
