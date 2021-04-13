using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public object Recipe { get; internal set; }

        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
            
        }
    }
}
