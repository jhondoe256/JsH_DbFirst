using DTOs;
using JacksSteakHouse_API.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerOrdercontroller : ControllerBase
    {
        private JacksSteakHouseContext _context;

        public CustomerOrdercontroller(JacksSteakHouseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomerOrder(CustomerOrderEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map
            var customerOrder = new CustomerOrder
            {
                OrderDate = model.OrderDate,
                CustomerId = model.CustomerId,
                MenuItemId = model.MenuItemId
            };

            //add to db
            await _context.CustomerOrders.AddAsync(customerOrder);
            //save
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerOrders()
        {
            var orders = await _context.CustomerOrders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.CustomerOrders.SingleOrDefaultAsync(o => o.OrderId == id);
            if (order is null)
                return BadRequest();

            return Ok(order);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, CustomerOrderEdit model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderInDb = await _context.CustomerOrders.SingleOrDefaultAsync(o => o.OrderId == id);

            if (orderInDb is null)
                return BadRequest();

            orderInDb.OrderDate = model.OrderDate;
            orderInDb.MenuItemId = model.MenuItemId;
            orderInDb.CustomerId = model.MenuItemId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderInDb = await _context.CustomerOrders.FirstOrDefaultAsync(o => o.OrderId == id);

            if (orderInDb is null)
                return BadRequest();

            _context.CustomerOrders.Remove(orderInDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("/PreviousOrders/{CustomerId}")]
        public async Task<IActionResult> GetCustomerOrders(int CustomerId)
        {
            var customerOrders = await _context.CustomerOrders
            .Include(co => co.Customer)
            .Include(co => co.MenuItem)
            .Where(co => co.CustomerId == CustomerId)
            .Select(co => new CustomerOrderListItem
            {
                OrderDate = co.OrderDate,
                CustomerFirstName = co.Customer.FirstName,
                CustomerLastName = co.Customer.LastName,
                MealName = co.MenuItem.MealName
            }).ToListAsync();

            if (customerOrders is null)
            {
                return NotFound();
            }

            return Ok(customerOrders);
        }
    }
}