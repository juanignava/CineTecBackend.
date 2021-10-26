using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CineTecBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly cinetecContext _context;
        public EmployeeController(cinetecContext context)
        {
            _context = context;
        }

        // Get all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            // Use raw SQL query to get all employees
            return await _context.Employees.FromSqlRaw(SqlQueries.GetAllEmployees).ToListAsync();

        }

        // Get a employee by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            // Use SQL query to get an specific employee
            var employee = await _context.Employees.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM EMPLOYEE
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            return employee;
        }

        // Post a employee
        [HttpPost]
        public async Task<ActionResult> Add(Employee employee)
        {
            // First look if the employee already exists
            var itemToAdd = await _context.Employees.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM EMPLOYEE
                                                                        WHERE Id = {employee.Id}").FirstOrDefaultAsync();
            if (itemToAdd != null)
                return Conflict();

            // Then add the employee to the database
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Update a employee
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, Employee employee)
        {
            // Look if the employee already exists
            var itemToUpdate = await _context.Employees.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM EMPLOYEE
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            if (itemToUpdate == null)
                return NotFound();

            // Define it's new values
            itemToUpdate.FirstName = employee.FirstName;
            itemToUpdate.LastName = employee.LastName;
            itemToUpdate.SecLastName = employee.SecLastName;
            itemToUpdate.Age = employee.Age;
            itemToUpdate.PhoneNumber = employee.PhoneNumber;
            itemToUpdate.Password = employee.Password;
            itemToUpdate.Role = employee.Role;
            // save the changes in the databse
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete a employee
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            // First look fot the employee with an sql query
             var itemToRemove = await _context.Employees.FromSqlInterpolated(@$"SELECT * 
                                                                        FROM EMPLOYEE
                                                                        WHERE Id = {id}").FirstOrDefaultAsync();

            if (itemToRemove == null)
                return NotFound();

            // Delete the employee using an SQL query
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"DELETE FROM EMPLOYEE 
                                                                WHERE Id = {id}");

            // Save changes in the database
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}