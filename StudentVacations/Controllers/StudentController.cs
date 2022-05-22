using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentVacations.Models;
using System.Security.Claims;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Cors;

namespace StudentVacations.Controllers
{
    //[Authorize]
    [EnableCors("corsapp")]
    //[Authorize(Roles = "Admin")]    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public StudentController(ApplicationContext context)
        {
            _db = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsItems()
        {
            //var students = await _db.Students.Include(x=>x.Сourses).Include(x => x.Vacations).ToListAsync();
            var students = await _db.Students
                .Include(x => x.Сourses)
                .Include(x => x.Vacations)
                .Select(x => new StudentDTO
                { 
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Email = x.Email,
                    CoursesCount = x.Сourses == null ? 0 : x.Сourses.Count(),
                    VacationsCount = x.Vacations == null ? 0 : x.Vacations.Count(),
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentsItem(long id)
        {
            var students = await _db.Students.FindAsync(id);

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentsItem(long id, Student students)
        {
            if (id != students.Id)
            {
                return BadRequest();
            }

            _db.Entry(students).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> StudentItem(Student student)
        {
            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetStudentsItem", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteTodoItem(long id)
        {
            var students = await _db.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            _db.Students.Remove(students);
            await _db.SaveChangesAsync();

            return students;
        }

        private bool StudentExists(long id)
        {
            return _db.Students.Any(e => e.Id == id);
        }
    }
}
