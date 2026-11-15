using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;
using MYAcademy_Portfolio.Helpers;
namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            var values = _context.Educations
                .ToList()
                .OrderByDescending(x => DurationHelper.EndYear(x.Duration))
                .ThenByDescending(x => DurationHelper.StartYear(x.Duration))
                .ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEducation(Education education)
        {
            _context.Educations.Add(education);
            _context.SaveChanges();
            return RedirectToAction("EducationList");
        }
        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            _context.Educations.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("EducationList");
        }
        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateEducation(Education education)
        {
            _context.Educations.Update(education);
            _context.SaveChanges();
            return RedirectToAction("EducationList");
        }
    }
}
