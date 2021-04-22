using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Models
{
    public class RecipeIngredient
    {

        public int RecipeId { get; set; }
        public Recipe RecipeName { get; set; }

        public int IngredientId { get; set; }
        public Ingredient IngredientName { get; set; }

        public RecipeIngredient()
        {
        }
        
    }
}
