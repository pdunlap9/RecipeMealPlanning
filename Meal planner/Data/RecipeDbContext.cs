using Meal_planner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Data
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<Recipe> Recipe { get; set; }
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) :base(options)
        {

        }
    }
}
