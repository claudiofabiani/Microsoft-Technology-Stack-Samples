using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Manager.Interface;
using DAL.Domain;
using DAL.Extension.Domain;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        
        private IStudentManager _studentManager;

        public StudentController(
            ILogger<StudentController> logger, 
            IStudentManager studentManager)
        {
            _logger = logger;

            _studentManager = studentManager;
        }
        // GET: api/Student
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedEnumerableDto<StudentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            PaginatedEnumerableDto<StudentDto> students = await _studentManager.ListAsync(2, 3, "EnrollmentDate", true, null, null, null, null, null, null, null, null, true);
            
            return Ok(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _studentManager.GetStudentByIdAsync(id);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Student
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
