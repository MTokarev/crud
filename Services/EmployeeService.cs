using AutoMapper;

using crud.Data;
using crud.DTOs;
using crud.Enums;
using crud.Interfaces;
using crud.Models;

namespace crud.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public EmployeeService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        private async Task<Employee> GetEmployeeByIdAsync(int id) => 
            await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
    }
}
