using FullstackAPI.Data;
using FullstackAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullstackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeesController(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees() 
        {
            var employees = await this.employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await this.employeeDbContext.AddAsync(employeeRequest);
            await this.employeeDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var emp = await this.employeeDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(emp == null) 
            {
                return NotFound();
            }

            return Ok(emp); 
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeRequest)
        {
            var emp = await this.employeeDbContext.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }

            emp.Name = updateEmployeRequest.Name;
            emp.Email = updateEmployeRequest.Email;
            emp.Phone = updateEmployeRequest.Phone;
            emp.Salary = updateEmployeRequest.Salary;
            emp.Department = updateEmployeRequest.Department;

            await this.employeeDbContext.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var emp = await this.employeeDbContext.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }

            this.employeeDbContext.Employees.Remove(emp);
            await this.employeeDbContext.SaveChangesAsync();

            return Ok(emp);

        }
    }
}
