using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Measurement { get; set; }


        public Ingredient()
        {

        }
        public Ingredient(string name, int quantity, string measurement)
        {
            Name = name;
            Quantity = quantity;
            Measurement = measurement;
        }

    }
}
