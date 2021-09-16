using System.Threading.Tasks;
using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Model.Authentication.AppRole;

namespace LivMoneyAPI.Repository.AuthenticationRepo {
    public interface IAuthRepo {
        Task<bool> IsAuthUserExist (string email);
        Task<AuthUser> AddAuth (AuthUser user);
        Task<AuthUser> getAuthById (int id);
        Task<AuthUser> getAuthUserByEmail (string email);
        Task<bool> IsUserNameExist (string UserName);
        Task<bool> IsTokenEmailExist (string email,string token);
        Task<AuthUser> getUserByToken (string email,string token);
        Task<AuthUser> UserLogin (string email,string password);

        //Assign Role Modal
         Task<UserRole> AddAuthRole (UserRole userRole);
         Task<UserRole> GetAuthRole (int userId);
    }
}