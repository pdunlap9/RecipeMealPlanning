using Meal_planner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.ViewModels
{
    public class AddRecipeViewModel
    {
        [Required(ErrorMessage = " Name of Recipe Required.")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = " Name must be between 3-30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Please Select an Option.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select ingredients or enter new ingredients.")]
        public List<SelectListItem> Ingredients { get; set; }

        [Required(ErrorMessage = "Please enter instructions.")]
        [StringLength(800, MinimumLength = 10, ErrorMessage = "Instructions must be more than ten characters!")]
        public string Instructions { get; set; }

        public AddRecipeViewModel(List<Recipe> recipes, List<Ingredient> ingredients)
        {
            Ingredients = new List<SelectListItem>();
            


            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(
                    new SelectListItem
                    {
                        Value = ingredient.Id.ToString(),
                        Text = ingredient.Name 
                        
                    });

            }
        }

        public AddRecipeViewModel() { }
    }
}
    

