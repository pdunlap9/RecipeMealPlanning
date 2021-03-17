using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Controllers
{
    public class HelloController : Controller
    {
        // GET:/<controller>/
        public IActionResult Index()
        {
            return Content("Hello  World");
        }
    }
}
