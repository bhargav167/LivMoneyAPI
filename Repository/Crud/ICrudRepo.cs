using System.Threading.Tasks;

namespace LivMoneyAPI.Repository.Crud {
    public interface ICrudRepo {
        void Add<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        Task<bool> SaveAll ();
    }
}