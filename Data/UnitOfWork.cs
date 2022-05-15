using crud.Abstractions;
using crud.Models;

namespace crud.Data
{
    /// <summary>
    /// Class brings a benefit when you have multiple repositories and contexts
    /// </summary>
    public class UnitOfWork
    {
        private readonly DataContext _context;
        private GenericRepository<Employee> _employeeRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return employee repository
        /// </summary>
        public GenericRepository<Employee> EmployeeRepository
        {
            get 
            {   // Initialize repo if not exist
                if (_employeeRepository == null)
                    _employeeRepository = new GenericRepository<Employee>(_context);
                
                return _employeeRepository;
            }
        }

        /// <summary>
        /// Only method to save db changes accross all repositories
        /// </summary>
        /// <returns>Returns number of affected rows from DataBase</returns>
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
