using crud.DTOs;
using crud.Models;

namespace crud.Interfaces
{
    public interface IEmployeeService
    {
        Task<Response<IList<EmployeeDto>>> GetAllAsync();
        Task<Response<EmployeeDto>> GetByIdAsync(int id);
        Task<Response<EmployeeDto>> UpdateAsync(EmployeeDto employeeDto);
        Task<Response<EmployeeDto>> CreateAsync(EmployeeDto employeeDto);
        Task<Response<EmployeeDto>> DeleteByIdAsync(int id);
    }
}