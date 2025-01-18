using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class DegreeVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Degree Name")]
        public string Name { get; set; }
    }
}
