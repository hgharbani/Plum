using System.ComponentModel;

namespace Plum.Model.Model.MAterial
{
    public class MaterialModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("کد")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("نام کالا")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ک شرکت")]
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("نام شرکت")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("قیمت هر کیلو")]
        public  double Price { get; set; }


    }

}
