using System.Threading.Tasks;
using LivMoneyAPI.Model.Authentication;

namespace LivMoneyAPI.Repository.AuthenticationRepo
{
    public interface IAuthRepo
    {
        Task<bool> IsAuthUserExist (string email);
        Task<AuthUser> AddAuth (AuthUser user);
        Task<AuthUser> getAuthById (int id);
        Task<AuthUser> getAuthUserByEmail (string email);
        Task<bool> IsUserNameExist (string UserName);
    }
}