using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using StudentVacations.Models;

namespace StudentVacations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Index1()
        {
            Сourse course = new Сourse();
            course.WeekNumberStart = 1;
            course.WeekNumberEnd = 2;
            course.Name = "";

            _db.Сourses.Add(course);
            _db.SaveChanges();

            course.Name = "Course" + course.Id;
            _db.SaveChanges();

            Student student = new Student();
            student.FirstName = "FirstName";
            student.Email = "email@gmail.com";
            student.Сourses = new List<Сourse>();
            student.Vacations = new List<Vacation>();
            //student.Сourses.Add(course);
            _db.Students.Add(student);
            _db.SaveChanges();
            student.FirstName = "FirstName" + student.Id;
            _db.SaveChanges();


            if (student.Сourses == null)
            {
                student.Сourses = new List<Сourse>();
            }

            if (student.Vacations == null)
            {
                student.Vacations = new List<Vacation>();
            }

            Vacation vacation = new Vacation();
            vacation.WeekNumberStart = 1;
            vacation.WeekNumberEnd = 2;
            vacation.Name = "";
            _db.Vacations.Add(vacation);
            _db.SaveChanges();

            vacation.Name = "Vacation" + vacation.Id;
            _db.SaveChanges();



            student.Сourses.Add(course);
            student.Vacations.Add(vacation);

            _db.SaveChanges();

            var students = _db.Students.ToList();
            var courses = _db.Сourses.ToList();
            var vacations = _db.Vacations.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}