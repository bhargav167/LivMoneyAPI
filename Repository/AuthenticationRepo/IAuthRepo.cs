using System.Threading.Tasks;
using LivMoneyAPI.Model.Authentication;

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
    }
}