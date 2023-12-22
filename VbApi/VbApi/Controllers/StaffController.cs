using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using VbApi.ValidationRules;
using System.ComponentModel.DataAnnotations.Schema;

namespace VbApi.Controllers
{
    public class Staff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal HourlySalary { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private static List<Staff> staffList = new List<Staff>(){
            new Staff
            {
                Name = "Ceren Ersoz",
                Email = "ersozceren2@gmail.com",
                Phone = "31328493",
                HourlySalary = 200
            },
        };



        [HttpGet]
        public ActionResult<IEnumerable<Staff>> Get()
        {
            return Ok(staffList);
        }



        [HttpGet("{id}")]
        public ActionResult<Staff> Get(int id)
        {
            Staff staff = staffList.FirstOrDefault(s => s.Id == id);

            if (staff == null)
                return NotFound();

            return Ok(staff);
        }



        [HttpPost]
        public ActionResult<Staff> Post([FromBody] Staff staff)
        {
            var validator = new StaffValidator();
            ValidationResult validationResult = validator.Validate(staff);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            staff.Id = staffList.Count + 1;
            staffList.Add(staff);

            return CreatedAtAction(nameof(Get), new { id = staff.Id }, staff);
        }



        [HttpPut("{id}")]
        public ActionResult<Staff> Put(int id, [FromBody] Staff updatedStaff)
        {
            Staff staff = staffList.FirstOrDefault(s => s.Id == id);

            if (staff == null)
                return NotFound();

            staff.Name = updatedStaff.Name;
            staff.Email = updatedStaff.Email;
            staff.Phone = updatedStaff.Phone;
            staff.HourlySalary = updatedStaff.HourlySalary;

            return Ok(staff);
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Staff staff = staffList.FirstOrDefault(s => s.Id == id);

            if (staff == null)
                return NotFound();

            staffList.Remove(staff);

            return NoContent();
        }
    }
}
