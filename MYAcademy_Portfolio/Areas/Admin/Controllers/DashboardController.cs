using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Areas.Admin.Models;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                // ── 1) İSTATİSTİK KARTLARI: Count ile say ──
                TotalProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                MessageCount = _context.Messages.Count(),
                UnreadMessageCount = _context.Messages.Count(x => !x.IsRead),

                // ── 2) SON 5 MESAJ: en yeni tarihten → ilk 5 ──
                LastMessages = _context.Messages
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList(),

                // ── 3) SON 3 PROJE: kategorisiyle birlikte, en yeni → ilk 3 ──
                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.ProjectId)
                    .Take(3)
                    .ToList(),

                // ── 4) KATEGORİ ÖZETİ: GroupBy ile kategori başına proje sayısı ──
                CategorySummaries = _context.Projects
                    .GroupBy(x => x.Category.CategoryName)
                    .Select(g => new CategorySummary
                    {
                        CategoryName = g.Key,
                        ProjectCount = g.Count()
                    })
                    .OrderByDescending(x => x.ProjectCount)   // en kalabalık kategori üstte
                    .ToList()
            };

            return View(model);
        }
    }
}