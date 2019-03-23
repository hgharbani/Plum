using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Utility
{
    public static class Extention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHashCode(this string s)
        {
            var data = Encoding.UTF8.GetBytes(s);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Encoding.UTF8.GetString(md5data);
            return hashedPassword;

        }
    }
}
