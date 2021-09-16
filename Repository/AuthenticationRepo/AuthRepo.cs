using System.Linq;
using System.Threading.Tasks;
using LivMoneyAPI.Data;
using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Model.Authentication.AppRole;
using Microsoft.EntityFrameworkCore;

namespace LivMoneyAPI.Repository.AuthenticationRepo {
    public class AuthRepo : IAuthRepo {
        private readonly DataContext _context;
        public AuthRepo (DataContext context) {
            _context = context;
        }
        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }
        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
                var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        public async Task<AuthUser> AddAuth (AuthUser user) {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash (user.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

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
            var loginUser = await _context.AuthUsers.Where (c => c.Email == email).FirstOrDefaultAsync ();
            if (loginUser == null)
                return null;

            if (loginUser.IsEmailConfirm == true) {
                if (!VerifyPasswordHash (password, loginUser.PasswordHash, loginUser.PasswordSalt))
                    return null;

                return loginUser;
            }
            return null;
        }

        //Assign Role User

        public async Task<UserRole> AddAuthRole (UserRole userRole) {
            await _context.UserRoles.AddAsync (userRole);
            await _context.SaveChangesAsync ();

            return userRole;
        }

        public async Task<UserRole> GetAuthRole(int userId)
        {
            var userRole=await _context.UserRoles.Include(c=>c.Role).FirstOrDefaultAsync(c=>c.AuthUserId==userId);
            return userRole;
        }
    }
}