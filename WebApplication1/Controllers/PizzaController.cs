using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Migrations;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using var _context = new PizzaContext();

            var data = _context.Pizzas.Include(p => p.Category).ToArray();

            return View(data);
        }

        public IActionResult Details(int id)
        {
            var _context = new PizzaContext();
            Pizza pizza = _context.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext _context = new PizzaContext())
            {
                PizzaFormModel model = new PizzaFormModel();
                List<Category> categories = _context.Categories.ToList();

                List<SelectListItem> listIngredients = new List<SelectListItem>();
                foreach(Ingredient ingredient in _context.Ingredients)
                {
                    listIngredients.Add(new SelectListItem()
                    { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                }
                model.Ingredients = listIngredients;
                model.Pizza = new Pizza();
                model.Categories = categories;
                return View("Create", model);
            }
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( PizzaFormModel data )
        {
			using PizzaContext _context = new PizzaContext();

            if (!ModelState.IsValid)
            {
                data.Categories = _context.Categories.ToList();
				return View("Create", data);
            }

            Pizza newPizza = new Pizza();
            newPizza = data.Pizza;

            if(data.SelectedIngredients != null)
            {
                foreach(string ingredientStrId in data.SelectedIngredients)
                {
                    int ingredientId = int.Parse(ingredientStrId);

                    Ingredient ingredient = _context.Ingredients.Where(i => i.Id == ingredientId).FirstOrDefault();
                    newPizza.Ingredients.Add(ingredient);
                
                }
            }
      
			_context.Pizzas.Add(newPizza);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            using var _context = new PizzaContext();

            Pizza pizzaToEdit = _context.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Ingredients).FirstOrDefault();
            if (pizzaToEdit == null)
            {
                return NotFound();
            }

			List<SelectListItem> listIngredients = new List<SelectListItem>();
			foreach (Ingredient ingredient in _context.Ingredients)
			{
				listIngredients.Add(new SelectListItem()
				{ Text = ingredient.Name, Value = ingredient.Id.ToString(), Selected = pizzaToEdit.Ingredients.Any(i => i.Id == ingredient.Id)});
			}


			var formModel = new PizzaFormModel
            {
                Pizza = pizzaToEdit,
                Categories = _context.Categories.ToList(),
                Ingredients = listIngredients
            };

			return View(formModel);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            using var _context = new PizzaContext();

            if (!ModelState.IsValid)
            {
                data.Categories = _context.Categories.ToList();
                List<Ingredient> ingredients = _context.Ingredients.ToList();
                List<SelectListItem> listIngredients = new List<SelectListItem>();
                foreach(Ingredient ing in ingredients)
                {
                    listIngredients.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString() });
                }
                data.Ingredients = listIngredients;
                data.Pizza.Id = id;

                return View("Update", data);
            }

            Pizza pizzaToEdit = _context.Pizzas.Where(pizza => pizza.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if(pizzaToEdit == null)
            {
                return NotFound();
            }

            pizzaToEdit.Name = data.Pizza.Name;
            pizzaToEdit.Price = data.Pizza.Price;
            pizzaToEdit.Description = data.Pizza.Description;
            pizzaToEdit.Image = data.Pizza.Image;

            pizzaToEdit.CategoryId = data.Pizza.CategoryId;
            pizzaToEdit.Ingredients.Clear();

            if(data.SelectedIngredients != null)
            {
                foreach(string ingStr in data.SelectedIngredients)
                {
                    int ingId = int.Parse(ingStr);
                    Ingredient ingredient = _context.Ingredients.Where(i => i.Id == ingId).FirstOrDefault();
                    pizzaToEdit.Ingredients.Add(ingredient);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            using var _context = new PizzaContext();
            Pizza pizzaToDelete = _context.Pizzas.Where(p => p.Id == id).FirstOrDefault();
            if(pizzaToDelete == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //API
        public IActionResult ApiIndex()
        {
            return View();
        }

        public IActionResult ApiCreate()
        {
            return View();
        }

        public IActionResult ApiUpdate(int id)
        {
            return View(id);
        }

        public IActionResult ApiDelete()
        {
            return View();
        }

    }
}
