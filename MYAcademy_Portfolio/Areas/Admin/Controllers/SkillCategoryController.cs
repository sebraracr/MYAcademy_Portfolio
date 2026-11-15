using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillCategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillCategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillCategoryList()
        {
            var values = _context.SkillCategories
                .Include(x => x.Skills)
                .ToList();
            return View(values);
        }
    }
}