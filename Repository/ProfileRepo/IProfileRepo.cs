using System.Threading.Tasks;
using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Model.Authentication.AppRole;

namespace LivMoneyAPI.Repository.ProfileRepo {
    public interface IProfileRepo {
        Task<AuthUser> getProfileById (int id);
         Task<UserRole> GetAuthRole (int userId);
    }
}