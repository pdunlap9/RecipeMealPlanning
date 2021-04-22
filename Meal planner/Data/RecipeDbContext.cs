using Meal_planner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Data
{
    public class RecipeDbContext : IdentityDbContext<IdentityUser>
    //DbContext
    {
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        




        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(r => new { r.RecipeId, r.IngredientId });

            base.OnModelCreating(modelBuilder);
        }
    }
}   

