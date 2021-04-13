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
            List<Recipe> recipes = context.Recipe.Include(r=> r.Category).ToList();

            return View(recipes);
            
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddRecipeViewModel addRecipeViewModel = new AddRecipeViewModel(context.Categories.ToList(), context.Ingredients.ToList());
            return View(addRecipeViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddRecipeViewModel addRecipeViewModel, string[] selectedIngredients)
        {
            if (ModelState.IsValid)
            {
                Recipe newRecipe = new Recipe
                {
                    Name = addRecipeViewModel.Name,
                    CategoryId = addRecipeViewModel.CategoryId,
                    Category = context.Categories.Find(addRecipeViewModel.CategoryId),
                    Instructions = addRecipeViewModel.Instructions
                };
                for (int i = 0; i < selectedIngredients.Length; i++)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient { RecipeId = newRecipe.Id, Recipe = newRecipe, IngredientId = Int32.Parse(selectedIngredients[i]) };
                    context.RecipeIngredient.Add(recipeIngredient);
                }
                context.Recipe.Add(newRecipe);
                context.SaveChanges();

                return Redirect("detail");
            }
            return View("Add", addRecipeViewModel);
        }
        public IActionResult Detail(int id)
        {
            Recipe theRecipe = context.Recipe
                .Include(r => r.Category)
                .Single(r => r.Id == id);
            List<RecipeIngredient> recipeIngredient = context.RecipeIngredient
                .Where(ri => ri.RecipeId == id)
                .Include(ri => ri.Ingredient)
                .ToList();
            RecipeDetailViewModel viewModel = new RecipeDetailViewModel(theRecipe, recipeIngredient);
            return View(viewModel);
        }
    }
}
