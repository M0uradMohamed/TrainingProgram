using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TrainingSpecialistVM
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Training Specialist Name")]
        public string Name { get; set; }
    }
}
