using System.ComponentModel.DataAnnotations;
using Pizzeria.Attributes;

namespace Pizzeria.Models
{
    public class Pizza
    {
        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a pizza name.")]
        [StringLength(25)]
        public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please provide a pizza price.")]
        [GreaterThanZero]
		public decimal Price { get; set; } = 0.0m;

		[Required(ErrorMessage = "Please provide a pizza description.")]
        [StringLength(255)]

		public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please provide an image.")]
        public string Image { get; set; } = string.Empty;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Ingredient>? Ingredients { get; set;}
    }
}
