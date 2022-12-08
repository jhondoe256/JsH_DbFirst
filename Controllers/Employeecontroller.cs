using DTOs;
using JacksSteakHouse_API.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Employeecontroller : ControllerBase
    {
        private JacksSteakHouseContext _context;

        public Employeecontroller(JacksSteakHouseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //mapping..
            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ApplicationSubmissionDate = model.ApplicationSubmissionDate,
                HireDate = model.HireDate,
                LocationId = model.LocationId
            };

            //add to db
            await _context.Employees.AddAsync(employee);
            //save
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeIDb = await _context.Employees.FindAsync(id);
            if (employeeIDb is null)
            {
                return NotFound();
            }

            employeeIDb.FirstName = model.FirstName;
            employeeIDb.LastName = model.LastName;
            employeeIDb.ApplicationSubmissionDate = model.ApplicationSubmissionDate;
            employeeIDb.LocationId = model.LocationId;
            employeeIDb.HireDate = model.HireDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeIDb = await _context.Employees.FindAsync(id);
            if (employeeIDb is null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeIDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}