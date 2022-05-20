using AutoMapper;

using crud.Data;
using crud.DTOs;
using crud.Enums;
using crud.Interfaces;
using crud.Models;
using crud.Options;

using Microsoft.Extensions.Options;

namespace crud.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EmployeeService(IMapper mapper, UnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<Response<IList<EmployeeDto>>> GetAllAsync()
        {
            var employeesFromDb = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var response = new Response<IList<EmployeeDto>>();

            if (employeesFromDb == null)
            {
                response.Error = new Error()
                {
                    ErrorType = ResponseErrorTypes.NotFound,
                    Message = "There are no employees registered in the database."
                };
            }
            else
            {
                response.Data = _mapper.Map<IList<EmployeeDto>>(employeesFromDb);
                response.Message = $"Found '{response.Data.Count}' employees.";
            }

            return response;
        }

        public async Task<Response<EmployeeDto>> GetByIdAsync(int id)
        {
            var employeeFromDb = await this.GetEmployeeByIdAsync(id);
            var response = new Response<EmployeeDto>();

            if (employeeFromDb == null)
            {
                response.Error = new Error()
                {
                    ErrorType = ResponseErrorTypes.NotFound,
                    Message = $"Unable to find eployee with {nameof(id)} '{id}'."
                };
            }
            else
            {
                response.Data = _mapper.Map<EmployeeDto>(employeeFromDb);
                response.Message = $"Employee with {nameof(employeeFromDb.Id)} '{employeeFromDb.Id}' has been found.";
            }
            
            return response;
        }

        public async Task<Response<EmployeeDto>> UpdateAsync(EmployeeDto employeeDto)
        {
            var employeeFromDb =  this
                .GetEmployeeByIdAsync(employeeDto.Id)
                .Result;
            var response = new Response<EmployeeDto>();

            if (employeeFromDb == null)
            {
                response.Error = new Error()
                {
                    ErrorType = ResponseErrorTypes.NotFound,
                    Message = $"Unable to find employee with {nameof(employeeDto.Id)} '{employeeDto.Id}'."
                };
            }
            else
            {
                employeeFromDb.Name = employeeDto.Name;
                employeeFromDb.Age = employeeDto.Age;
                employeeFromDb.UpdatedOnUtc = DateTime.UtcNow;
                
                _unitOfWork.EmployeeRepository.Update(employeeFromDb);
                await _unitOfWork.SaveAsync();
                
                response.Data = _mapper.Map<EmployeeDto>(employeeFromDb);
                response.Message = $"Employee with {nameof(employeeFromDb.Id)} '{employeeFromDb.Id}' has been updated";
            }

            return response;
        }

        public async Task<Response<EmployeeDto>> CreateAsync(EmployeeDto employeeDto)
        {
            var employeeToCreate = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            };

            var employeeTracking = await _unitOfWork.EmployeeRepository.AddAsync(employeeToCreate);
            var rowAffected = await _unitOfWork.SaveAsync();

            var response = new Response<EmployeeDto>();

            if (rowAffected > 0)
            {
                response.Data = _mapper.Map<EmployeeDto>(employeeTracking.Entity);
                response.Message = $"New employee with {nameof(employeeTracking.Entity.Id)} '{employeeTracking.Entity.Id}' has been created.";
            }
            else
            {
                response.Error = new Error()
                {
                    ErrorType = ResponseErrorTypes.ServiceError,
                    Message = $"Service error. Unable to create new employee with {nameof(employeeDto.Name)} '{employeeDto.Name}'."
                };
            }

            return response;

        }

        public async Task<Response<EmployeeDto>> DeleteByIdAsync(int id)
        {
            var employeeFromDB = await this.GetEmployeeByIdAsync(id);
            var response = new Response<EmployeeDto>();

            if (employeeFromDB == null)
            {
                response.Error = new Error()
                {
                    ErrorType = ResponseErrorTypes.NotFound,
                    Message = $"Unable to find employee with {nameof(employeeFromDB.Id)} '{id}'."
                };
            }
            else
            {
                _unitOfWork.EmployeeRepository.Remove(employeeFromDB);
                int rowAffected = await _unitOfWork.SaveAsync();

                if (rowAffected > 0)
                {
                    response.Data = _mapper.Map<EmployeeDto>(employeeFromDB);
                    response.Message = $"Employee with {nameof(employeeFromDB.Id)} '{id}' has been removed.";
                }
                else
                {
                    response.Error = new Error()
                    {
                        ErrorType = ResponseErrorTypes.ServiceError,
                        Message = $"Unable to remove empoloyee with {nameof(employeeFromDB.Id)} '{id}'"
                    };
                }
            }

            return response;
        }

        public async Task<Response<Pagination<EmployeeDto>>> GetPagedEmployeesAsync(PageParams pageParams)
        {

            // Apply default params from config if values are incorrect
            pageParams.PageSize = pageParams.PageSize > 0 && pageParams.PageSize <= _paginationOptions.MaxPageSize?
                pageParams.PageSize :
                _paginationOptions.DefaultPageSize; 

            pageParams.PageIndex = pageParams.PageIndex > 0 ?
                pageParams.PageIndex :
            _paginationOptions.DefaultPageIndex;

            // Check if pageSize is bigger than total employees in DB
            int totalEmployees = await _unitOfWork.EmployeeRepository.CountAsync();
            pageParams.PageSize = pageParams.PageSize > totalEmployees ?
                totalEmployees :
                pageParams.PageSize;

            // Check how many pages available in DB based on requested page size
            // Round result using ceiling to include items on the last page
            int pagesCount = (int)Math.Ceiling((decimal)totalEmployees / (decimal)pageParams.PageSize);
            
            var response = new Response<Pagination<EmployeeDto>>();

            // If requested page index is out of bound return error
            if (pageParams.PageIndex > pagesCount)
            {
                response.Error = new Error()
                {
                    ErrorType = ResponseErrorTypes.NotFound,
                    Message = $"Requested page is out of bound. {nameof(totalEmployees)}: '{totalEmployees}', {nameof(pageParams.PageIndex)}: '{pageParams.PageIndex}', {nameof(pageParams.PageSize)}: '{pageParams.PageSize}'."
                };
            }
            else
            // Otherwise create and return paginated result
            {
                var pagedEmployees = await _unitOfWork
                    .EmployeeRepository
                    .GetPagedEmployeesAsync(pageParams.PageIndex, pageParams.PageSize, _paginationOptions.MaxPageSize, totalEmployees);
                
                response.Data = _mapper.Map<Pagination<EmployeeDto>>(pagedEmployees);
                response.Message = $"Paged result has been generated for {nameof(pageParams.PageIndex)}: '{pageParams.PageIndex}'. Employees returned: '{pagedEmployees.TotalItems}'.";
            }

            return response;
        }

        public Task<int> TotalEmployees()
        {
            return _unitOfWork.EmployeeRepository.CountAsync();
        }

        private async Task<Employee> GetEmployeeByIdAsync(int id) => 
            await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

    }
}
