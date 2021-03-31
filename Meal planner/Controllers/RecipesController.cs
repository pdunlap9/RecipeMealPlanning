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
                    Instructions = addRecipeViewModel.Instructions
                };
                context.Recipe.Add(newRecipe);
                context.SaveChanges();

                return Redirect("/Recipes");
            }
            return View(addRecipeViewModel);
        }

    }
}
