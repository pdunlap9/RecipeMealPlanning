using Meal_planner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.ViewModels
{
    public class AddRecipeIngredientViewModel
    {
        [Required(ErrorMessage = "Recipe is required")]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Ingredient is required")]
        public int IngredientId { get; set; }

        public Recipe Recipe { get; set; }

        public List<SelectListItem> Ingredient { get; set; }

        public AddRecipeIngredientViewModel(Recipe theRecipe, List<Ingredient> possibleIngredients)
        {
            Ingredient = new List<SelectListItem>();

            foreach (var ingredient in possibleIngredients)
            {
                Ingredient.Add(new SelectListItem
                {
                    Value = ingredient.Id.ToString(),
                    Text = ingredient.Name
                });
            }

            Recipe = theRecipe;
        }

        public AddRecipeIngredientViewModel()
        {
        }
    }
}
