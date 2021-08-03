using System.Threading.Tasks;
using LivMoneyAPI.Model.Authentication;

namespace LivMoneyAPI.Repository.AuthenticationRepo
{
    public class AuthRepo : IAuthRepo
    {
        public Task<AuthUser> AddAuth(AuthUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthUser> getAuthById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthUser> getAuthUserByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsAuthUserExist(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUserNameExist(string UserName)
        {
            throw new System.NotImplementedException();
        }
    }
}