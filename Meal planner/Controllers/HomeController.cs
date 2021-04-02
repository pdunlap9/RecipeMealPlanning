using Meal_planner.Data;
using Meal_planner.Models;
using Meal_planner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class HomeController : Controller
    {
        private RecipeDbContext context;

        public HomeController(RecipeDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Recipe> recipe = context.Recipe.ToList();

            return View(recipe);
        }

        [HttpGet("/Add")]
        public IActionResult AddRecipe()
        {
            AddRecipeViewModel addRecipeViewModel = new AddRecipeViewModel(context.Recipe.ToList(), context.Ingredients.ToList());


            return View(addRecipeViewModel);
        }
    }
}
        
