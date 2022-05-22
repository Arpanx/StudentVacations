using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.EntityFrameworkCore;
using StudentVacations.Models;
using System.Security.Claims;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace StudentVacations.Controllers
{
    //[Authorize]
    [EnableCors("corsapp")]
    //[Authorize(Roles = "Admin")]    
    [Route("api/[controller]")]
    [ApiController]
    public class CarListController : ControllerBase
    {
        // The Web API will only accept tokens 1) for users, and 
        // 2) having the access_as_user scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        private readonly ApplicationContext _db;

        public CarListController(ApplicationContext context)
        {
            _db = context;
        }

        // GET: api/CarList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Сourse>>> GetCarsItems()
        {
            Сourse course = new Сourse();
            course.WeekNumberStart = 1;
            course.WeekNumberEnd = 2;
            _db.Сourses.Add(course);
            _db.SaveChanges();

            course.Name = "Course" + course.Id;
            _db.SaveChanges();

            Student student = new Student();
            student.FirstName = "FirstName";
            student.Email = "email@gmail.com";
            //student.Сourses = new List<Сourse>();
            student.Сourses.Add(course);
            _db.Students.Add(student);
            _db.SaveChanges();
            student.FirstName = "FirstName" + student.Id;
            _db.SaveChanges();


            if (student.Сourses?.Any() ?? false)
            {
                
            }

            _db.SaveChanges();

            var students = await _db.Students.ToListAsync();

            var students2 = await _db.Students
                .Select(x => new
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Email = x.Email,
                    //СoursesCount = x?.Сourses ?? x.Сourses.Count(),
                })
                .ToListAsync();

            var courses2 = await _db.Сourses
                .Select(x => new
                {
                    Id = x.Id,
                    WeekNumberStart = x.WeekNumberStart,
                    WeekNumberEnd = x.WeekNumberEnd,
                    Name = x.Name
                })
                .ToListAsync();

            //        survey.QuestionList
            //.Where(l => l.Questions != null)
            //.SelectMany(l => l.Questions)
            //.Where(q => q != null && q.AnswerRows != null)
            //.SelectMany(q => q.AnswerRows);
            //.SelectMany(x => x ?? Enumerable.Empty<SomeLongTypeName>())

            var items = students
                        //.Where(x=>x.Сourses != null)
                        .SelectMany(students => students?.Сourses ?? Enumerable.Empty<Сourse>(), (students, course) => new { students, course })
                        .Select(x => new
                        {
                            StudentsId = x.students?.Id,
                            FirstName = x.students?.FirstName,
                            Email = x.students?.Email,
                            CourseId = x?.course?.Id,
                            WeekNumberStart = x?.course?.WeekNumberStart,
                            WeekNumberEnd = x?.course?.WeekNumberEnd,
                            CourseName = x?.course?.Name,
                        })
                        .ToArray();

            var t = new { students2, courses2, items };

            return Ok(t);

            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var items = await _db.Cars.ToListAsync();

            //List<TodoItem> uu = new();
            //var tt = new TodoItem();
            //tt.Description = "Description";
            //uu.Add(tt);

            //return uu.ToList();

            //List<Car> uu = new();
            //var car = new Car();
            //car.CarTextRu = "CarTextRu=" + items.Count();
            //car.CreatedBy = "system";
            //uu.Add(car);
            //_db.Cars.Add(car);
            //_db.SaveChanges();
            
            //var items1 = await _db.Cars.ToListAsync();
            //var car2 = new Car();
            //car2.CreatedBy = "system";
            //car2.CarTextRu = "CarTextRu=" + items1.Count();
            //_db.Cars.Add(car2);
            //_db.SaveChanges();

            //uu.Add(car2);

            //var items2 = await _db.Cars.ToListAsync();

            //return items2.ToList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetCarsItem(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            //var todoItem = await _context.TodoItems.FindAsync(id);

            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            return null;
            //return todoItem;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        //{
        //    HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

        //    if (id != todoItem.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(todoItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TodoItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        //{
        //    HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        //    string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    todoItem.Owner = owner;
        //    todoItem.Status = false;

        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        //}

        // DELETE: api/TodoItems/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        //{
        //    HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

        //    var todoItem = await _context.TodoItems.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TodoItems.Remove(todoItem);
        //    await _context.SaveChangesAsync();

        //    return todoItem;
        //}

        //private bool CarsExists(int id)
        //{
        //    return _context.Cars.Any(e => e.Id == id);
        //}
    }
}
