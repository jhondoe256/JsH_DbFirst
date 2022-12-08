using DTOs;
using JacksSteakHouse_API.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Customercontroller : ControllerBase
    {
        private JacksSteakHouseContext _context;

        public Customercontroller(JacksSteakHouseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            //Map
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            //add to db
            await _context.Customers.AddAsync(customer);
            //save
            await _context.SaveChangesAsync();
            return Ok($"Customer {customer.FirstName} - {customer.LastName} was Created!");
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerInDb = await _context.Customers.FindAsync(id);
            if (customerInDb is null)
            {
                return NotFound();
            }

            customerInDb.FirstName = model.FirstName;
            customerInDb.LastName = model.LastName;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerInDb = await _context.Customers.FindAsync(id);
            if (customerInDb is null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}