using Meal_planner.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class RecipesController : Controller
    {
        static private List<Recipe> Recipes = new List<Recipe>();
        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.recipes = Recipes;
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Route("/Recipes/Add")]
        public IActionResult NewEvent(string name, string description)
        {
            Recipes.Add(new Recipe(name, description));
            return Redirect("/Recipes");
        }

    }
}
