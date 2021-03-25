using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Meal_planner.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }


        public Recipe()
        {
        }

        public Recipe(string name, string description)
        {
            Name = name;
            Description = description;
            
        }


        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Recipe recipe &&
                   Id == recipe.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
