using Meal_planner.Data;
using Meal_planner.Models;
using Meal_planner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private RecipeDbContext context;

        public SearchController(RecipeDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = AllRecipesController.ColumnChoices;
            return View();
        }

        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Recipe> recipes;
            List<RecipeDetailViewModel> displayRecipes = new List<RecipeDetailViewModel>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                recipes = context.Recipe
                   .Include(r => r.Category)
                   .ToList();

                foreach (var recipe in recipes)
                {
                    List<RecipeIngredient> recipeIngredient = context.RecipeIngredient
                        .Where(ri => ri.RecipeId == recipe.Id)
                        .Include(ri => ri.IngredientName)
                        .ToList();

                    RecipeDetailViewModel newDisplayRecipe = new RecipeDetailViewModel(recipe, recipeIngredient);
                    displayRecipes.Add(newDisplayRecipe);
                }
            }
            else
            {
                if (searchType == "category")
                {
                    recipes = context.Recipe
                        .Include(r => r.Category)
                        .Where(r => r.Category.Name == searchTerm)
                        .ToList();

                    foreach (Recipe recipe in recipes)
                    {
                        List<RecipeIngredient> recipeIngredients = context.RecipeIngredient
                        .Where(ri => ri.RecipeId == recipe.Id)
                        .Include(ri => ri.IngredientName)
                        .ToList();

                        RecipeDetailViewModel newDisplayRecipe = new RecipeDetailViewModel(recipe, recipeIngredients);
                        displayRecipes.Add(newDisplayRecipe);
                    }

                }
                else if (searchType == "ingredient")
                {
                    List<RecipeIngredient> recipeIngredients = context.RecipeIngredient
                        .Where(r => r.IngredientName.Name == searchTerm)
                        .Include(r => r.RecipeName)
                        .ToList();

                    foreach (var recipe in recipeIngredients)
                    {
                        Recipe foundRecipe = context.Recipe
                            .Include(r => r.Category)
                            .Single(r => r.Id == recipe.RecipeId);

                        List<RecipeIngredient> displayIngredients = context.RecipeIngredient
                            .Where(ri => ri.RecipeId == foundRecipe.Id)
                            .Include(ri => ri.IngredientName)
                            .ToList();

                        RecipeDetailViewModel newDisplayRecipe = new RecipeDetailViewModel(foundRecipe, displayIngredients);
                        displayRecipes.Add(newDisplayRecipe);
                    }
                }
            }

            ViewBag.columns = AllRecipesController.ColumnChoices;
            ViewBag.title = "Recipes with " + AllRecipesController.ColumnChoices[searchType] + ": " + searchTerm;
            ViewBag.recipes = displayRecipes;

            return View("Index");
        }
    }
}
