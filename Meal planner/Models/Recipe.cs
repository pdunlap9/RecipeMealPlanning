using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Meal_planner.Models
{
    public class Recipe :IEnumerable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        

        public Recipe()
        {
        }

        public Recipe(string name, string description, string instructions, string ingredients)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Instructions = instructions;
            
        }
       
        public IEnumerator GetEnumerator()
        {
            List<Recipe> _recipe = new List<Recipe>();
        
            foreach (var recipe in _recipe)
            {
                yield return (recipe);
            }
        

        }
    }
}
