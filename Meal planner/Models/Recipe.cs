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

        public Recipe(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
