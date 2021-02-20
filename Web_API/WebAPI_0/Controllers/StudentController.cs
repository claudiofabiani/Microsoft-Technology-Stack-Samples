using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Manager.Interface;
using DAL.Domain;
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
        public async Task<IEnumerable<Student>> Get()
        {
            IEnumerable<Student> s = await _studentManager.ListAsync(2, 3, "EnrollmentDate", true, null, null, null, null, null, null, null, null, true);
            return s;
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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
