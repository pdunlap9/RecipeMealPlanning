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
        //[Required(ErrorMessage = " Name of Recipe Required.")]
        //[StringLength(30, MinimumLength = 4, ErrorMessage = " Name must be between 3-30 characters.")]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        //[Required(ErrorMessage = " Please Select an Option.")]
        public List<SelectListItem> Category { get; set; }

        //[Required(ErrorMessage = "Please select ingredients or enter new ingredients.")]
        public List<Ingredient> Ingredients { get; set; }

        public int IngredientId { get; set; }

        //[Required(ErrorMessage = "Please enter instructions.")]
        //[StringLength(800, MinimumLength = 10, ErrorMessage = "Instructions must be more than ten characters!")]
        public string Instructions { get; set; }
        

        public AddRecipeViewModel(List<Category> categories, List<Ingredient> ingredients)
        {
            Category = new List<SelectListItem>();



            foreach (var category in categories)
            {
                Category.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name

                    });

            }
            Ingredients = ingredients;
        }

        public AddRecipeViewModel() { }
    }
}
    

