using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;

namespace Pizzeria.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaContext _context;

        public PizzaController(PizzaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Pizza> pizzas = _context.Pizzas;
            return Ok(pizzas);
        }

        [HttpGet("{filter}")]
        public IActionResult Get(string filter)
        {
            IQueryable<Pizza> pizzas = _context.Pizzas.Where(p => p.Name.Contains(filter));
            return Ok(pizzas);
        }

        [HttpPost]
        public IActionResult CreatePizza(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();

            return Ok(pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pizza edit)
        {
            Pizza record = _context.Pizzas.FirstOrDefault(p => p.Id == id);
            if (record is null)
            {
                return NotFound();
            }

            record.Name = edit.Name;
            record.Description = edit.Description;
            record.Price = edit.Price;
            record.CategoryId = edit.CategoryId;
            record.Ingredients = edit.Ingredients;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pizza record = _context.Pizzas.FirstOrDefault(p => p.Id == id);
            if(record is null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(record);
            _context.SaveChanges();

            return Ok();
        }
    }
}
