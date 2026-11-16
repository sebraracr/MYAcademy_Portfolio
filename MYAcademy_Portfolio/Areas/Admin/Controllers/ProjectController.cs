using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using MYAcademy_Portfolio.Areas.Admin.Models;
using MYAcademy_Portfolio.Entities;
namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;

        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ProjectList()
        {
            var values = _context.Projects
                .Include(x => x.Category)
                .Include(x => x.Skills).OrderByDescending(x => x.ProjectId).ToList();
            return View(values);
        }
    }
}