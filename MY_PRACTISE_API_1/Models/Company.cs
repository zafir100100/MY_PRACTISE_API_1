using System;
using System.Collections.Generic;

#nullable disable

namespace MY_PRACTISE_API_1.Models
{
    public partial class Company
    {
        public Company()
        {
            Departments = new HashSet<Department>();
        }

        public decimal CompanyId { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
