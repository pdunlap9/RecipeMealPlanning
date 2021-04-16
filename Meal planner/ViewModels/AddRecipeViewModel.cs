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
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public List<SelectListItem> Category { get; set; }

        public List<Ingredient> Ingredients { get; set; }

       // public string Instructions { get; set; }
        

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
    

