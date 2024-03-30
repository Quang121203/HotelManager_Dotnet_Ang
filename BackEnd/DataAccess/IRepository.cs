using System.Linq.Expressions;

namespace BackEnd.DataAccess
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predecate);


        Task<List<T>> GetAsync(int indexPage, int pageSize);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predecate, int indexPage, int pageSize);


        Task<int> CountAsync(int pageSize);
        Task<int> CountAsync(Expression<Func<T, bool>> predecate, int pageSize);

        Task<T> GetSingleAsync(object id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predecate);

        Task InsertAsync(T item);
        void Update(T entity);
        Task<bool> DeleteAsync(object id);

    }
}
