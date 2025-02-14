using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
