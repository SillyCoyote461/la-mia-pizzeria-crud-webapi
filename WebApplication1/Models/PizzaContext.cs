using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Pizzeria.Models
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzadb;Integrated Security=True;TrustServerCertificate=True");
        }
        public void Seed()
        {
            //Pizza seeder
            if (!Pizzas.Any())
            {
                var seed = new Pizza[]
                {
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "Margherita",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 4.5m
                    },
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "Diavola",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 5.5m
                    },
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "San Daniele",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 6m
                    },
                    new Pizza
                    {
                        Image = "https://picsum.photos/200",
                        Name = "Quattro stagioni",
                        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Price = 6m
                    }

                };
                Pizzas.AddRange(seed);
                SaveChanges();
            }

            //Category seeder
            if (!Categories.Any())
            {
                var seed = new Category[]
                {
                    new Category
                    {
                        Name = "Classica"
                    },
                    new Category
                    {
                        Name = "Rossa"
                    },
                    new Category
                    {
                        Name = "Bianca"
                    },
                    new Category
                    {
                        Name = "Vegana"
                    },
                    new Category
                    {
                        Name = "Gluten free"
                    },
                };
                Categories.AddRange(seed);
                SaveChanges();
			}

            //Ingredients seeder
            if (!Ingredients.Any())
            {
                var seed = new Ingredient[]
                {
                    new Ingredient
                    {
                        Name = "Mozzarella"
                    },
                    new Ingredient
                    {
                        Name = "Prosciutto"
                    },
                    new Ingredient
                    {
                        Name = "Funghi Porcini"
                    },
                    new Ingredient
                    {
                        Name = "Funghi"
                    },
                    new Ingredient
                    {
                        Name = "Sardine"
                    },
                    new Ingredient
                    {
                        Name = "Burrata"
                    },
                    new Ingredient
                    {
                        Name = "Bufala"
                    }
                };
                Ingredients.AddRange(seed);
                SaveChanges();
            }
        }

    }
}