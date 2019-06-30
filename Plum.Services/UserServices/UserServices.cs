using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Plum.Data;
using Plum.Data.Contex;
using Plum.Model.Model;

namespace Plum.Services.UserServices
{
   public  class UserServices : IUserServices
   {
       private PlumContext db;

        public UserServices(PlumContext context)
        {
            db = context;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="pasword"></param>
       /// <returns></returns>
       public User GetUser(User user)
       {
           return db.Users.AsNoTracking().FirstOrDefault(a =>(a.UserName.ToLower() == user.UserName && a.Password == user.Password)||a.UserId==user.UserId );
       }

       public PlumResult AddUser(User user)
       {
           var result=new PlumResult();
           var userName = GetUser(user);
           if (userName != null)
           {
               db.Entry(user).State = EntityState.Added;
               result.Message="کاربر تعریف شد";
               return result;
           }

           result.IsChange = false;
           result.Message = "کاربر تکراری میباشد";
           return result;
        }

       public PlumResult UpdateUser(User user)
       {
           var result = new PlumResult();
           var userName = GetUser(user);
           var data = Encoding.ASCII.GetBytes(user.Password);
           user.UserName = userName.UserName;
           var md5 = new MD5CryptoServiceProvider();
           var md5data = md5.ComputeHash(data);
           var hashedPassword = Encoding.UTF8.GetString(md5data);
           user.Password = hashedPassword;
           db.Entry(user).State = EntityState.Modified;

               result.Message = "کاربر تعریف شدرمز عبور با موفقیت تغییر  یافت";
               return result;
          
       }
    }
}
