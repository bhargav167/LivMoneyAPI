using System.Threading.Tasks;
using LivMoneyAPI.Data;
using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Model.Authentication.AppRole;
using Microsoft.EntityFrameworkCore;

namespace LivMoneyAPI.Repository.ProfileRepo {
    public class ProfileRepo : IProfileRepo {
        private readonly DataContext _context;
        public ProfileRepo (DataContext context) {
            _context = context;
        }
        public async Task<AuthUser> getProfileById (int id) {
            var AuthUser = await _context.AuthUsers.FirstOrDefaultAsync (u => u.Id == id);
            return AuthUser;
        }
        public async Task<UserRole> GetAuthRole (int userId) {
            var userRole = await _context.UserRoles.Include (c => c.Role).FirstOrDefaultAsync (c => c.AuthUserId == userId);
            return userRole;
        }
    }
}