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
    }
}
