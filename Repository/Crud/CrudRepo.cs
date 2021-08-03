using System.Threading.Tasks;
using LivMoneyAPI.Data;

namespace LivMoneyAPI.Repository.Crud
{
    public class CrudRepo : ICrudRepo
    {
        private readonly DataContext _context;
        public CrudRepo (DataContext context) {
            _context = context;
        }
         public void Add<T> (T entity) where T : class {
            _context.Add (entity);
        }

        public void Delete<T> (T entity) where T : class {
            _context.Remove (entity);
        }
        public async Task<bool> SaveAll () {
            return await _context.SaveChangesAsync () > 0;
        }
    }
}