//using Meal_planner.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Meal_planner.ViewModels
//{
//    public class AddIngredientViewModel
//    {
//        [Required(ErrorMessage = "Ingredient is required")]
//        public int RecipeId { get; set; }
        
//        public int IngredientId { get; set; }

//        public Recipe Recipe { get; set; }

//        public List<SelectListItem> Ingredients { get; set; }

//        public AddIngredientViewModel(Recipe theRecipe, List<Ingredient> possibleIngredients)
//        {
//            Ingredients = new List<SelectListItem>();

//            foreach (var ingredient in possibleIngredients)
//            {
//                Ingredients.Add(new SelectListItem
//                {
//                    Value = ingredient.Id.ToString(),
//                    Text = ingredient.Name
//                });
//            }

//            Recipe = theRecipe;
//        }

//        public AddIngredientViewModel()
//        {
//        }
//    }
//}
