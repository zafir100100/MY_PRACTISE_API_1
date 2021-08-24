using System;
using System.Collections.Generic;

#nullable disable

namespace MY_PRACTISE_API_1.Models
{
    public partial class Designation
    {
        public Designation()
        {
            Employees = new HashSet<Employee>();
        }

        public decimal DesignationId { get; set; }
        public string DesignationName { get; set; }
        public decimal DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
