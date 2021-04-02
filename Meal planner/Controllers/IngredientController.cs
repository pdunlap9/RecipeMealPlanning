using Meal_planner.Data;
using Meal_planner.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class IngredientController : Controller
    {
        private RecipeDbContext context;
        public IngredientController(RecipeDbContext dbContext)
        {
            context = dbContext;
        }

        //GET:/<controller>/
        public IActionResult Index()
        { 
            List<Ingredient> ingredients = context.Ingredients.ToList();
            return View(ingredients);
        }
        public IActionResult Add()
        {
            Ingredient ingredient = new Ingredient();
            return View(ingredient);
        }
        [HttpPost]
        public IActionResult Add(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                context.Ingredients.Add(ingredient);
                context.SaveChanges();
                return Redirect("/Ingredient/");
            }

            return View("Add", ingredient);
        }
    }
}
