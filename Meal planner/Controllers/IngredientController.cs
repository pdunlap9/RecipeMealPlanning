using Meal_planner.Data;
using Meal_planner.Models;
using Meal_planner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return Redirect("add");
            }

            return View("Add", ingredient);
        }
        public IActionResult AddRecipe(int id)
        {
            Recipe theRecipe = context.Recipe.Find(id);
            List<Ingredient> possibleIngredients = context.Ingredients.ToList();
            AddRecipeIngredientViewModel viewModel = new AddRecipeIngredientViewModel(theRecipe, possibleIngredients);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddRecipe(AddRecipeIngredientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                int recipeId = viewModel.RecipeId;
                int ingredientId = viewModel.IngredientId;

                List<RecipeIngredient> existingItems = context.RecipeIngredient
                    .Where(ri => ri.RecipeId == recipeId)
                    .Where(js => js.IngredientId == ingredientId)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient
                    {
                        RecipeId = recipeId,
                        IngredientId = ingredientId
                    };
                    context.RecipeIngredient.Add(recipeIngredient);
                    context.SaveChanges();
                }

                return Redirect("/Home/Detail/" + recipeId);
            }

            return View(viewModel);
        }

        public IActionResult About(int id)
        {
            List<RecipeIngredient> recipeIngredients = context.RecipeIngredient
                .Where(ri => ri.IngredientId == id)
                .Include(ri => ri.RecipeName)
                .Include(ri => ri.IngredientName)
                .ToList();

            return View(recipeIngredients);
        }
    }
}
