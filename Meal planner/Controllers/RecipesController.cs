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
        [HttpGet]
        public IActionResult Edit(int Id)
        {

            Recipe oldRecipe = context.Recipe.Find(Id);

            ViewBag.ingredients = context.Ingredients.ToList();
            EditRecipeViewModel editRecipeViewModel = new EditRecipeViewModel(oldRecipe, context.Ingredients.ToList(), context.Categories.ToList())
            {

                RecipeId = oldRecipe.Id,
                Name = oldRecipe.Name,
                CategoryId = oldRecipe.CategoryId,
                RecipeIngredient = oldRecipe.RecipeIngredients,
                Ingredients = context.Ingredients.ToList(),


            };

            //editRecipeViewModel.Id = oldRecipe.Id;
            return View(editRecipeViewModel);
            //go to recipe rep find by id to pop viewmodel
        }

        /* [HttpPost]
         public async Task<IActionResult> Edit(EditRecipeViewModel editRecipeViewModel)
         {
             Recipe oldRecipe = context.Recipe.Find(editRecipeViewModel.Id);
             oldRecipe.Id = editRecipeViewModel.Id;
             oldRecipe.Name = editRecipeViewModel.Name;
             oldRecipe.RecipeIngredients = editRecipeViewModel.RecipeIngredients;

             context.Recipe.Update(oldRecipe);

             //pass view model. this will take changes in form, set equal to old id, update this. 
             return RedirectToAction("Index", "Recipes");
         }*/

        [HttpPost]
        public IActionResult Edit(Recipe EditedRecipe, EditRecipeViewModel editRecipeViewModel, string[] selectedIngredients)
        {
            if (ModelState.IsValid)
            {
                EditedRecipe = new Recipe
                {
                    Id = editRecipeViewModel.RecipeId,
                    Name = editRecipeViewModel.Name,
                    CategoryId = editRecipeViewModel.CategoryId,
                    Category = context.Categories.Find(editRecipeViewModel.CategoryId),
                    //RecipeIngredients = editRecipeViewModel.RecipeIngredient,
                };
            for (int i = 0; i < selectedIngredients.Length; i++)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient { RecipeId = EditedRecipe.Id, RecipeName = EditedRecipe, IngredientId = Int32.Parse(selectedIngredients[i]) };
                context.RecipeIngredient.Add(recipeIngredient);
                }

                context.Entry(EditedRecipe).State = EntityState.Modified;
                context.SaveChanges();
               // context.Recipe.Update(EditedRecipe);
                

                return Redirect("Index");
        }
            return View("Edit", editRecipeViewModel);
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
                    RecipeIngredient recipeIngredient = new RecipeIngredient { RecipeId = newRecipe.Id, RecipeName = newRecipe, IngredientId = Int32.Parse(selectedIngredients[i]) };
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
