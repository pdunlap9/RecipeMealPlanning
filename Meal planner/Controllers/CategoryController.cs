using Meal_planner.Data;
using Meal_planner.Models;
using Meal_planner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class CategoryController : Controller
    {
        private RecipeDbContext context;

        public CategoryController(RecipeDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> category = context.Categories.ToList();
            return View(category);
        }

        //public IActionResult Add()
        //{
        // AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
        // return View(addCategoryViewModel);
        // }
        [HttpPost]
       
        public IActionResult Detail(int id)
        {
            Category theCategory = context.Categories
                .Single(c => c.Id == id);

            return View(theCategory);
        }
    }
}
