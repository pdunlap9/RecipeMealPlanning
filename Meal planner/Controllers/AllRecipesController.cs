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
    public class AllRecipesController : Controller
    {
        internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"Category", "category"},
            {"Ingredient", "ingredient"}
        };

        internal static List<string> TableChoices = new List<string>()
        {
            "category",
            "ingredient"
        };

        private RecipeDbContext context;

        public AllRecipesController(RecipeDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ColumnChoices;
            ViewBag.tablechoices = TableChoices;
            ViewBag.categories = context.Categories.ToList();
            ViewBag.ingredients = context.Ingredients.ToList();
            return View();
        }

        //list recipes by column and value
        public IActionResult Recipes(string column, string value)
        {
            List<Recipe> recipes = new List<Recipe>();
            List<RecipeDetailViewModel> displayRecipes = new List<RecipeDetailViewModel>();

            if (column.ToLower().Equals("all"))
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

                ViewBag.title = "All Recipes";
            }
            else
            {
                if (column == "category")
                {
                    recipes = context.Recipe
                        .Include(r => r.Category)
                        .Where(r => r.Category.Name == value)
                        .ToList();

                    foreach (Recipe recipe in recipes)
                    {
                        List<RecipeIngredient> recipeIngredient = context.RecipeIngredient
                        .Where(ri => ri.RecipeId == recipe.Id)
                        .Include(ri => ri.IngredientName)
                        .ToList();

                        RecipeDetailViewModel newDisplayRecipe = new RecipeDetailViewModel(recipe, recipeIngredient);
                        displayRecipes.Add(newDisplayRecipe);
                    }
                }
                else if (column == "ingredient")
                {
                    List<RecipeIngredient> recipeIngredient = context.RecipeIngredient
                        .Where(r => r.IngredientName.Name == value)
                        .Include(r => r.RecipeName)
                        .ToList();

                    foreach (var recipe in recipeIngredient)
                    {
                        Recipe foundRecipe = context.Recipe
                            .Include(r => r.Category)
                            .Single(r => r.Id == recipe.RecipeId);

                        List<RecipeIngredient> displayIngredient = context.RecipeIngredient
                            .Where(ri => ri.RecipeId == foundRecipe.Id)
                            .Include(ri => ri.IngredientName)
                            .ToList();

                        RecipeDetailViewModel newDisplayRecipe = new RecipeDetailViewModel(foundRecipe, displayIngredient);
                        displayRecipes.Add(newDisplayRecipe);
                    }
                }
                ViewBag.title = "Recipes with " + ColumnChoices[column] + ": " + value;
            }
            ViewBag.recipes = displayRecipes;

            return View();
        }
    }
}
