using Meal_planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.ViewModels
{
    public class RecipeDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        //public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public int CategoryId { get; set; }

        public RecipeDetailViewModel(Recipe theRecipe, List<RecipeIngredient> recipeIngredients)
        {
            Id = theRecipe.Id;
            Name = theRecipe.Name;
            CategoryName = theRecipe.Category.Name;
            //Instructions = theRecipe.Instructions;
            

            Ingredients = "";
            for (int i = 0; i < recipeIngredients.Count; i++)
            {
                Ingredients += recipeIngredients[i].IngredientName.Name;
                if (i < recipeIngredients.Count - 1)
                {
                    Ingredients += ", ";
                }
            }
        }
    }
}
