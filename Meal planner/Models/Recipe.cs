using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Meal_planner.Models
{
    public class Recipe 
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Id { get; set; }
        public string Instructions { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

        public Recipe()
        {
        }

        public Recipe(string name, string instructions)
        {
            Name = name;
            Instructions = instructions;

            
        }
       
        

        
    }
}
