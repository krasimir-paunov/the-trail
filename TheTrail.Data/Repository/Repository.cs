using Microsoft.EntityFrameworkCore;
using TheTrail.Data.Interfaces;

namespace TheTrail.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TheTrailDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TheTrailDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> All()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> AllAsNoTracking()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }
    }
}