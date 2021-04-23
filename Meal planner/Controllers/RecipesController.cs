using Meal_planner.Data;
using Meal_planner.Models;
using Meal_planner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    [Authorize]
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
            

            List<Recipe> recipes = context.Recipe
                .Include(r => r.Category)
                .ToList();

            return View(recipes);

        }
        [HttpGet("/Add")]
        public IActionResult Add()
        {
            AddRecipeViewModel addRecipeViewModel = new AddRecipeViewModel(context.Categories.ToList(), context.Ingredients.ToList());
            return View(addRecipeViewModel);
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

                };
                for (int i = 0; i < selectedIngredients.Length; i++)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient { RecipeId = newRecipe.Id, RecipeName = newRecipe, IngredientId = Int32.Parse(selectedIngredients[i]) };
                    context.RecipeIngredient.Add(recipeIngredient);
                }
                context.Recipe.Add(newRecipe);
                context.SaveChanges();

                return Redirect("Index");
            }
            return View("Add", addRecipeViewModel);
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {

            Recipe oldRecipe = context.Recipe.Find(Id);

            
            EditRecipeViewModel editRecipeViewModel = new EditRecipeViewModel(context.Categories.ToList(), context.Ingredients.ToList())
            {

                RecipeId = oldRecipe.Id,
                Name = oldRecipe.Name,
                CategoryId = oldRecipe.CategoryId,
                RecipeIngredient = context.RecipeIngredient.Where(r => r.RecipeId == oldRecipe.Id)
                                                            .Include(r => r.IngredientName)
                                                            .ToList(),
                Ingredients = context.Ingredients.ToList(),


            };

            editRecipeViewModel.RecipeId = oldRecipe.Id; 
            return View(editRecipeViewModel);
        }


        [HttpPost]
        public IActionResult EditRecipe(EditRecipeViewModel editRecipeViewModel, string[] selectedIngredients)
        {
            if (ModelState.IsValid)
            {

                Recipe editedRecipe = context.Recipe.Find(editRecipeViewModel.RecipeId);
                  editedRecipe.Name = editRecipeViewModel.Name;
                  editedRecipe.CategoryId = editRecipeViewModel.CategoryId;
                  editedRecipe.Category = context.Categories.Find(editRecipeViewModel.CategoryId);
                


                //remove index of ingredients of recipe go into recipeingredientID(Database) before add neww ingredients remove anything that currently exsts wiuth current recipe ID
                //Lambda? recipeIngredient obj context.recipeingredients where  context.remove that recipeingredient (While loop?) remove all then go into selected ingredients and add them all back in
                

                for (int i = 0; i < selectedIngredients.Length; i++)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient { RecipeId = editedRecipe.Id, RecipeName = editedRecipe, IngredientId = Int32.Parse(selectedIngredients[i]) };
                    context.RecipeIngredient.Update(recipeIngredient);
                    context.RecipeIngredient.Add(recipeIngredient);
                    context.SaveChanges();
            }

                context.Recipe.Update(editedRecipe);
                context.SaveChanges();
            

            return Redirect("Index");
        }
            return View("Edit", editRecipeViewModel);
    }

        
        public IActionResult Detail(int id)
        {
            Recipe aRecipe = context.Recipe
                .Include(r => r.Category)
                .Single(r => r.Id == id);
            List<RecipeIngredient> recipeIngredient = context.RecipeIngredient
                .Where(ri => ri.RecipeId == id)
                .Include(ri => ri.IngredientName)
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
