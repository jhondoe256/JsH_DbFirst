using DTOs;
using JacksSteakHouse_API.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemcontroller : ControllerBase
    {
        private JacksSteakHouseContext _context;

        public MenuItemcontroller(JacksSteakHouseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostMenuItem(MenuItemEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Map...
            var item = new MenuItem
            {
                MealName = model.MealName,
                MealDescription = model.MealDescription,
                Price = model.Price
            };

            //add to db
            await _context.MenuItems.AddAsync(item);
            //save
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return Ok(menuItems);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, MenuItemEdit model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var menuItemInDb = await _context.MenuItems.FindAsync(id);
            if (menuItemInDb == null)
            {
                return NotFound();
            }
            menuItemInDb.MealDescription = model.MealDescription;
            menuItemInDb.MealName = model.MealName;
            menuItemInDb.Price = model.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var menuItemInDb = await _context.MenuItems.FindAsync(id);
            if (menuItemInDb == null)
            {
                return NotFound();
            }

            _context.MenuItems.Remove(menuItemInDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}