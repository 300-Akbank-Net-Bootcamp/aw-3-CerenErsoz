using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using VbApi.ValidationRules;
using System.ComponentModel.DataAnnotations.Schema;

namespace VbApi.Controllers
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double HourlySalary { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employeeList = new List<Employee>(){
            new Employee
            {
                Name = "Ceren Ersoz",
                DateOfBirth = new DateTime(2000, 9, 3),
                Email = "ersozceren2@gmail.com",
                Phone = "31328493",
                HourlySalary = 200
            },
        };



        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(employeeList);
        }



        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }



        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            var validator = new EmployeeValidator();
            ValidationResult validationResult = validator.Validate(employee);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            employee.Id = employeeList.Count + 1;
            employeeList.Add(employee);

            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }



        [HttpPut("{id}")]
        public ActionResult<Employee> Put(int id, [FromBody] Employee updatedEmployee)
        {
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound();

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Phone = updatedEmployee.Phone;
            employee.HourlySalary = updatedEmployee.HourlySalary;

            return Ok(employee);
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound();

            employeeList.Remove(employee);

            return NoContent();
        }
    }
}



