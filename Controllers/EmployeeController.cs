using crud.DTOs;
using crud.Enums;
using crud.Interfaces;
using crud.Models;

using Microsoft.AspNetCore.Mvc;

namespace crud.Controllers
{
    /// <summary>
    /// Employee controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        { 
            _employeeService = employeeService;
        }

        /// <summary>
        /// Returns all employees
        /// </summary>
        /// <returns>Return a list of employees DTO</returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeService.GetAllAsync();

            return this.GenerateResponse(response);
        }

        /// <summary>
        /// Gets employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Return employee DTO</returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var response = await _employeeService.GetByIdAsync(id);

            return this.GenerateResponse(response);
        }

        /// <summary>
        /// Updates employee
        /// </summary>
        /// <param name="employeeDto">EmployeeDto to update</param>
        /// <returns>Updated employee DTO</returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest($"Please provide a valid {nameof(employeeDto)}.");

            var response = await _employeeService.UpdateAsync(employeeDto);

            return this.GenerateResponse(response);
        }

        /// <summary>
        /// Creates an employee
        /// </summary>
        /// <param name="employeeDto">EmployeeDto to create</param>
        /// <returns>Employee DTO</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var response = await _employeeService.CreateAsync(employeeDto);

            if (response.HasError)
            {
                return response.Error.ErrorType == ResponseErrorTypes.NotFound ?
                   NotFound(response.Error.Message) :
                   BadRequest(response.Error.Message);
            }

            // This will return HTTP 201 using 'Route Get/{id}' and created object
            return CreatedAtRoute(response.Data.Id, response.Data);

        }

        /// <summary>
        /// Deletes employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Deleted employee DTO</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(int id)
        {
            var response = await _employeeService.DeleteByIdAsync(id);

            return this.GenerateResponse(response);
        }

        /// <summary>
        /// Helper method to process response
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="response">Response from employee service</param>
        /// <returns>Formatted IActionResult</returns>
        private IActionResult GenerateResponse<T>(Response<T> response) 
            where T : class
        {
            if (response.HasError)
            {
                return response.Error.ErrorType == ResponseErrorTypes.NotFound ?
                   NotFound(response.Error.Message) :
                   BadRequest(response.Error.Message);
            }

            return Ok(response.Data);
        }
    }
}
