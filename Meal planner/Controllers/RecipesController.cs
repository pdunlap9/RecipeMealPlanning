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
            List<Recipe> recipes = context.Recipe.Include(r => r.Category).ToList();

            return View(recipes);

        }
        [HttpGet("/Add")]
        public IActionResult Add()
        {
            AddRecipeViewModel addRecipeViewModel = new AddRecipeViewModel(context.Categories.ToList(), context.Ingredients.ToList());
            return View(addRecipeViewModel);
        }

        public IActionResult Edit(int RecipeId)
        {
            Recipe oldRecipe = context.Recipe.Find(RecipeId);

            EditRecipeViewModel editRecipeViewModel = new EditRecipeViewModel
            {
                
                Id = oldRecipe.Id,
                Name = oldRecipe.Name,
                CategoryId = oldRecipe.CategoryId,
                RecipeIngredients = oldRecipe.RecipeIngredients,
                
            };
            return View(editRecipeViewModel );
            //go to recipe rep find by id to pop viewmodel
        }

        [HttpPost]
        public IActionResult SubmitEdit(EditRecipeViewModel editRecipeViewModel, int recipeId)
        {
            Recipe oldRecipe = context.Recipe.Find(recipeId);
            oldRecipe.Id = editRecipeViewModel.Id;
            oldRecipe.Name = editRecipeViewModel.Name;
            oldRecipe.RecipeIngredients = editRecipeViewModel.RecipeIngredients;

            context.Recipe.Update(oldRecipe);
            context.SaveChanges();
            //pass view model. this will take changes in form, set equal to old id, update this. 
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddRecipe(AddRecipeViewModel addRecipeViewModel, string[] selectedIngredients)
        {
            if (ModelState.IsValid)
            {
                Recipe newRecipe = new Recipe
                {
                    Name = addRecipeViewModel.Name,
                    CategoryId = addRecipeViewModel.CategoryId,
                    Category = context.Categories.Find(addRecipeViewModel.CategoryId),
                    //Instructions = addRecipeViewModel.Instructions
                };
                for (int i = 0; i < selectedIngredients.Length; i++)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient { RecipeId = newRecipe.Id, Recipe = newRecipe, IngredientId = Int32.Parse(selectedIngredients[i]) };
                    context.RecipeIngredient.Add(recipeIngredient);
                }
                context.Recipe.Add(newRecipe);
                context.SaveChanges();

                return Redirect("Index");
            }
            return View("Add", addRecipeViewModel);
        }
        public IActionResult Detail(int id)
        {
            Recipe aRecipe = context.Recipe
                .Include(r => r.Category)
                .Single(r => r.Id == id);
            List<RecipeIngredient> recipeIngredient = context.RecipeIngredient
                .Where(ri => ri.RecipeId == id)
                .Include(ri => ri.Ingredient)
                .ToList();
            RecipeDetailViewModel viewModel = new RecipeDetailViewModel(aRecipe, recipeIngredient);
            return View(viewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.recipes = context.Recipe.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] recipeIds)
        {
            foreach (int recipeId in recipeIds)
            {
                Recipe theRecipe = context.Recipe.Find(recipeId);
                context.Recipe.Remove(theRecipe);
            }
            context.SaveChanges();

            return Redirect("Index");
        }


    }
}
