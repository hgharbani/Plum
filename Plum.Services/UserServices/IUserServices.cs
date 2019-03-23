using Plum.Data;
using Plum.Model.Model;

namespace Plum.Services.UserServices
{
    public interface IUserServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pasword"></param>
        /// <returns></returns>
        User GetUser(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        PlumResult AddUser(User user);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        PlumResult UpdateUser(User user);
    }
}