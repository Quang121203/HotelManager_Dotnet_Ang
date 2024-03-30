namespace BackEnd.DataAccess
{
    using System.Linq.Expressions;
    using BackEnd.Models.DAL;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T> where T : class, new()
    {

        private readonly AppDBContext context;
        public Repository(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<int> CountAsync(int pageSize)
        {
            var page = await context.Set<T>().CountAsync() * 1.0 / pageSize;
            return (int)Math.Ceiling(page);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predecate, int pageSize)
        {
            var page = await context.Set<T>().CountAsync(predecate) * 1.0 / pageSize;
            return (int)Math.Ceiling(page);
        }



        public async Task<bool> DeleteAsync(object id)
        {
            var existing = await context.Set<T>().FindAsync(id);
            if (existing != null)
            {
                context.Set<T>().Remove(existing);
                return true;
            }
            return false;
        }

        public async Task<List<T>> GetAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predecate)
        {
            return await context.Set<T>().Where(predecate).ToListAsync();
        }

        public async Task<List<T>> GetAsync(int indexPage, int pageSize)
        {
            return await context.Set<T>().Skip((indexPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predecate, int indexPage, int pageSize)
        {
            return await context.Set<T>().Where(predecate).Skip((indexPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetSingleAsync(object id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predecate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predecate);
        }

        public async Task InsertAsync(T item)
        {
            await context.Set<T>().AddAsync(item);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

    }
}
