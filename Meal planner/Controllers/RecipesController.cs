using Meal_planner.Data;
using Meal_planner.Models;
using Meal_planner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class RecipesController : Controller
    {
        private RecipeDbContext context;
        public RecipesController(RecipeDbContext dbContext)
        {
            context = dbContext;
        }

        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Recipe> recipes = context.Recipe.ToList();
            return View(recipes);
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddRecipeViewModel addRecipeViewModel = new AddRecipeViewModel();
            return View(addRecipeViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddRecipeViewModel addRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                Recipe newRecipe = new Recipe
                {
                    Name = addRecipeViewModel.Name,
                    Description = addRecipeViewModel.Description,
                    Quantity = addRecipeViewModel.Quantity,
                    Measurement = addRecipeViewModel.Measurement,
                    Item = addRecipeViewModel.Item
                    // Ingredients = addRecipeViewModel.Ingredients,
                    // Directions = addRecipeViewModel.Directions
                };
                context.Recipe.Add(newRecipe);
                context.SaveChanges();

                return Redirect("/Recipes");
            }
            return View(addRecipeViewModel);
        }

        //Should I add this to the above maybe?
        [HttpPost]
        public ActionResult Index(AddRecipeViewModel addRecipeViewModel)
        {
            //This is where I messed up, need to figure out how to add the ingredients to the newRecipe list I think..

            List<Recipe> _Ingredients = new List<Recipe>();
            if (ModelState.IsValid)
            {
                //Loop through the forms
                for (int i = 0; i <= Request.Form.Count; i++)
                {
                    var Quantity = Request.Form["Quantity[" + i + "]"];
                    var Measurement = Request.Form["Measurement[" + i + "]"];
                    var Item = Request.Form["Item[" + i + "]"];

                    //IsValid or something similar?

                    if (Quantity == Quantity)
                    {
                        _Ingredients.Add(new Recipe { Quantity = Quantity, Measurement = Measurement, Item = Item });
                        return Redirect("/Recipes");
                    }
                }
            }

                return View(addRecipeViewModel);
            
        }
    }
}


////Create Single recipe have mult. Items added.
//NoContentResult save ing. button
//Lists of strings.
