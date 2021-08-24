using System.Linq;
using System.Threading.Tasks;
using LivMoneyAPI.Data;
using LivMoneyAPI.Model.Authentication;
using Microsoft.EntityFrameworkCore;

namespace LivMoneyAPI.Repository.AuthenticationRepo {
    public class AuthRepo : IAuthRepo {
        private readonly DataContext _context;
        public AuthRepo (DataContext context) {
            _context = context;
        }
        public async Task<AuthUser> AddAuth (AuthUser user) {
            await _context.AuthUsers.AddAsync (user);
            await _context.SaveChangesAsync ();

            return user;
        }

        public async Task<AuthUser> getAuthById (int id) {
            var AuthUser = await _context.AuthUsers.FirstOrDefaultAsync (u => u.Id == id);
            return AuthUser;
        }

        public async Task<AuthUser> getAuthUserByEmail (string email) {
            var AuthUser = await _context.AuthUsers.FirstOrDefaultAsync (u => u.Email == email);
            return AuthUser;
        }

        public async Task<AuthUser> getUserByToken (string email, string token) {
            var AuthUser = await _context.AuthUsers.FirstOrDefaultAsync (u => u.Email == email && u.Token == token);
            return AuthUser;
        }

        public async Task<bool> IsAuthUserExist (string email) {
            if (await _context.AuthUsers.AnyAsync (e => e.Email == email))
                return true;

            return false;
        }

        public async Task<bool> IsTokenEmailExist (string email, string token) {
            if (await _context.AuthUsers.AnyAsync (e => e.Email == email && e.Token == token))
                return true;

            return false;
        }

        public async Task<bool> IsUserNameExist (string UserName) {
            if (await _context.AuthUsers.AnyAsync (e => e.UserName == UserName))
                return true;

            return false;
        }

        public async Task<AuthUser> UserLogin (string email, string password) {
            var loginUser = await _context.AuthUsers.Where (c => c.Email == email && c.Password == password).FirstOrDefaultAsync ();
            return loginUser;
        }
    }
}