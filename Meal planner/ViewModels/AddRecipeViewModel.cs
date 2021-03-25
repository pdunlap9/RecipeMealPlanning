using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_planner.ViewModels
{
    public class AddRecipeViewModel
    {
        [Required(ErrorMessage = " Name of Recipe Required.")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = " Name must be between 3-30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Please Select an Option.")]
        public string Description { get; set; }
    }
}
