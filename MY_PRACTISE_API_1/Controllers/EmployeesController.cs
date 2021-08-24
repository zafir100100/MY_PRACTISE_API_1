using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY_PRACTISE_API_1.Models;

namespace MY_PRACTISE_API_1.Controllers
{
    //public class PaginationInput
    //{
    //    public int? Skip { get; set; }
    //    public int? Take { get; set; }
    //}

    public class DateRangeInput
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ModelContext _context;

        public EmployeesController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(decimal id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(decimal id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        public class Report1Output
        {
            public decimal EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string DesignationName { get; set; }
            public string DepartmentName { get; set; }
            public string CompanyName { get; set; }
            public DateTime JoiningDate { get; set; }
            public decimal Salary { get; set; }
        }

        [HttpGet("report1")]
        public async Task<ActionResult<List<Report1Output>>> GetEmployeesByActiveStatusTrue()
        {
            List<Report1Output> query = await (from e in _context.Employees.Where(i=> i.ActiveStatus == 1)
                                               join des in _context.Designations
                                                   on e.DesignationId equals des.DesignationId
                                               join dept in _context.Departments
                                                   on des.DepartmentId equals dept.DepartmentId
                                               join c in _context.Companies
                                                   on dept.CompanyId equals c.CompanyId
                                               select new Report1Output
                                               {
                                                   EmployeeId = e.EmployeeId,
                                                   EmployeeName = e.EmployeeName,
                                                   DesignationName = des.DesignationName,
                                                   DepartmentName = dept.DepartmentName,
                                                   CompanyName = c.CompanyName,
                                                   JoiningDate = e.JoiningDate,
                                                   Salary = e.Salary
                                               }).ToListAsync();
            return query;
        }

        [HttpPost("report2")]
        public async Task<ActionResult<List<Report1Output>>> GetEmployeesByDateRange([FromBody] DateRangeInput input)
        {
            List<Report1Output> query = await (from e in _context.Employees.Where(i => i.JoiningDate >= input.DateFrom && i.JoiningDate <= input.DateTo && i.ActiveStatus == 1)
                                               join des in _context.Designations
                                                   on e.DesignationId equals des.DesignationId
                                               join dept in _context.Departments
                                                   on des.DepartmentId equals dept.DepartmentId
                                               join c in _context.Companies
                                                   on dept.CompanyId equals c.CompanyId
                                               select new Report1Output
                                               {
                                                   EmployeeId = e.EmployeeId,
                                                   EmployeeName = e.EmployeeName,
                                                   DesignationName = des.DesignationName,
                                                   DepartmentName = dept.DepartmentName,
                                                   CompanyName = c.CompanyName,
                                                   JoiningDate = e.JoiningDate,
                                                   Salary = e.Salary
                                               }).ToListAsync();
            return query;
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(decimal id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(decimal id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
