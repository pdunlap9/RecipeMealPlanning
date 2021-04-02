using Meal_planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.ViewModels
{
    public class RecipeDetailViewModel
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }

        public RecipeDetailViewModel(Recipe theRecipe, List<Ingredient> recipeIngredients)
        {
            RecipeId = theRecipe.Id;
            Name = theRecipe.Name;
            Instructions = theRecipe.Instructions;
            

            Ingredients = "";
            for (int i = 0; i < recipeIngredients.Count; i++)
            {
                Ingredients += recipeIngredients[i].Name;
                if (i < recipeIngredients.Count - 1)
                {
                    Ingredients += ", ";
                }
            }
        }
    }
}
