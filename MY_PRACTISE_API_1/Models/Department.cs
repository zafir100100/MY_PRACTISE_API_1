using System;
using System.Collections.Generic;

#nullable disable

namespace MY_PRACTISE_API_1.Models
{
    public partial class Department
    {
        public Department()
        {
            Designations = new HashSet<Designation>();
        }

        public decimal DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
    }
}
