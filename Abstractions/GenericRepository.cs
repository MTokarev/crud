using crud.Data;
using crud.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Linq.Expressions;

namespace crud.Abstractions
{
    public class GenericRepository<T> : IDisposable
        where T : class
    {
        private bool _disposed;
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public EntityEntry<T> Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return _context.Update(entity);
        }

        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _context.AddAsync(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<Pagination<T>> GetPagedEmployeesAsync(int pageIndex, int pageSize, int pageSizeMaxAllowed, int totalEmployees)
        {
            var result = await _context
                .Set<T>()
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();
            
            return new Pagination<T>(pageIndex, pageSize, pageSizeMaxAllowed, totalEmployees, result);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}
