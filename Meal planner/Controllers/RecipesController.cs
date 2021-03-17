using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class RecipesController : Controller
    {
        static private List<string> Recipes = new List<string>();
        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            Recipes.Add("Chicken Carbonara");
            Recipes.Add("Macaroni and Cheese");
            Recipes.Add("Yellow Chicken Curry");
            ViewBag.recipes = Recipes;
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

    }
}
