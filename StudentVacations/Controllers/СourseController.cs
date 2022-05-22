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
    public class CourseController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public CourseController(ApplicationContext context)
        {
            _db = context;
        }

        // GET: api/Сourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<СourseDTO>>> GetСourseItems()
        {
            var courses = await _db.Сourses
                .Select(x => new СourseDTO
                { 
                    Id = x.Id,
                    WeekNumberStart = x.WeekNumberStart,
                    WeekNumberEnd = x.WeekNumberEnd,
                    Name = x.Name,
                    StudentId = x.StudentId
                  
                })
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/Сourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Сourse>> GetСourseItem(long id)
        {
            var courses = await _db.Сourses.FindAsync(id);

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // GET: api/Сourse/byStudentId?id=1
        [HttpGet("byStudentId")]
        public async Task<ActionResult<IEnumerable<Сourse>>> Get(int id)
        {
            var courses = await _db.Сourses.Where(x => x.StudentId == id).ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // PUT: api/Сourse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutСourseItem(long id, Сourse courses)
        {
            if (id != courses.Id)
            {
                return BadRequest();
            }

            var items = _db.Сourses
                .Where(x => x.StudentId == courses.StudentId)
                .Where(x => x.Id != courses.Id)
                .AsNoTracking()
                .ToList();

            items.Add(courses);

            bool overlap = items
            .Any(r => items
                 .Where(q => q != r)
                 .Any(q => q.WeekNumberEnd >= r.WeekNumberStart && q.WeekNumberStart <= r.WeekNumberEnd));

            if (overlap)
            {
                return BadRequest();
            }

            _db.Entry(courses).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!СourseExists(id))
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

        // POST: api/Сourse
        [HttpPost]
        public async Task<ActionResult<Сourse>> СourseItem(Сourse courses)
        {
            var items = _db.Сourses
                .Where(x => x.StudentId == courses.StudentId)
                .AsNoTracking()
                .ToList();

            items.Add(courses);

            bool overlap = items
            .Any(r => items
                 .Where(q => q != r)
                 .Any(q => q.WeekNumberEnd >= r.WeekNumberStart && q.WeekNumberStart <= r.WeekNumberEnd));

            if (overlap)
            {
                return BadRequest();
            }

            _db.Сourses.Add(courses);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetСourseItem", new { id = courses.Id }, courses);
        }

        // DELETE: api/Сourse/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Сourse>> DeleteСourseItem(long id)
        {
            var courses = await _db.Сourses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _db.Сourses.Remove(courses);
            await _db.SaveChangesAsync();

            return courses;
        }

        private bool СourseExists(long id)
        {
            return _db.Сourses.Any(e => e.Id == id);
        }
    }
}
