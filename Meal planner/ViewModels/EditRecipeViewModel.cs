using Meal_planner.Data;
using Meal_planner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.ViewModels
{
    public class EditRecipeViewModel : AddRecipeViewModel
    {
        // public string Name { get; set; }
        // public List<Ingredient> Ingredients { get; set; }
        // public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
    
        public List<RecipeIngredient> RecipeIngredient { get; set; }
        public int RecipeId { get; set; }


        public EditRecipeViewModel()
        {

        }

       /* public EditRecipeViewModel(Recipe theRecipe, List<Ingredient> possibleIngredients, List<Category> categories)
        {
            Ingredient = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            RecipeId = theRecipe.Id;

            foreach (var category in categories)
            {
                Categories.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name

                    });

                foreach (var ingredient in possibleIngredients)
                {
                    Ingredient.Add(new SelectListItem
                    {
                        Value = ingredient.Id.ToString(),
                        Text = ingredient.Name
                       
                    });
                    //theRecipe.RecipeIngredients = Ingredient;
                }

                RecipeName = theRecipe;
            }
*/






        public EditRecipeViewModel( List<Category> categories, List<Ingredient> ingredients)
            {
                Categories = new List<SelectListItem>();


                foreach (var category in categories)
                {
                    Categories.Add(
                        new SelectListItem
                        {
                            Value = category.Id.ToString(),
                            Text = category.Name

                        });

                }
                Ingredients = ingredients;
            }


        }
    }


