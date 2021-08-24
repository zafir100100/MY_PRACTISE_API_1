using System;
using System.Collections.Generic;

#nullable disable

namespace MY_PRACTISE_API_1.Models
{
    public partial class Employee
    {
        public decimal EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal DesignationId { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public decimal ActiveStatus { get; set; }

        public virtual Designation Designation { get; set; }
    }
}
