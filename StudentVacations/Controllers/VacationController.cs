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
using StudentVacations.Models.Service;

namespace StudentVacations.Controllers
{
    //[Authorize]
    [EnableCors("corsapp")]
    //[Authorize(Roles = "Admin")]    
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public VacationController(ApplicationContext context)
        {
            _db = context;
        }

        // GET: api/Vacations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacationDTO>>> GetVacationItems()
        {
            var courses = await _db.Vacations
                .Select(x => new VacationDTO
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

        // GET: api/Vacation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacation>> GetVacationItem(long id)
        {
            var courses = await _db.Vacations.FindAsync(id);

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // GET: api/Vacation/byStudentId?id=1
        [HttpGet("byStudentId")]
        public async Task<ActionResult<IEnumerable<Vacation>>> Get(int id)
        {
            var courses = await _db.Vacations.Where(x => x.StudentId == id).ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // PUT: api/Vacation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacationItem(long id, Vacation courses)
        {
            if (id != courses.Id)
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

        // POST: api/Vacation
        [HttpPost]
        public async Task<ActionResult<Vacation>> VacationItem(Vacation vacation)
        {
            
            // Основной метод где реализовал бизнес логику !!!
            
            var courses = _db.Сourses.Where(x => x.StudentId == vacation.StudentId)
                .AsNoTracking()
                .ToList();

            var vacationService = new VacationService();

            try
            {
                // Тут проверки на допустимость операции и соблюдение бизнес-логики
                var courseCurrent = vacationService.TryAddVacationItem(vacation, courses);

                _db.Database.BeginTransaction();
                _db.Entry(courseCurrent).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                // Проверяем есть ли перекрытия по срокам уже в базе (дополнительная проверка по факту)
                bool overlap = _db.Сourses.Where(x => x.StudentId == vacation.StudentId).AsNoTracking()
                   .Any(r => _db.Сourses.Where(x => x.StudentId == vacation.StudentId).AsNoTracking()
                        .Where(q => q != r)
                        .Any(q => q.WeekNumberEnd >= r.WeekNumberStart && q.WeekNumberStart <= r.WeekNumberEnd));

                if (overlap)
                {
                    // string message = "Бизнес-правило #5 Перенос срока окончания курса конфликтует с другими курсами";
                    
                    _db.Database.CurrentTransaction.Rollback();

                    return BadRequest();
                }

                // Все хорошо, можно сохранить отпуск
                _db.Vacations.Add(vacation);
                await _db.SaveChangesAsync();
                _db.Database.CurrentTransaction.Commit();

                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return BadRequest();
        }

        // DELETE: api/Сourse/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Сourse>> DeleteVacationItem(long id)
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
