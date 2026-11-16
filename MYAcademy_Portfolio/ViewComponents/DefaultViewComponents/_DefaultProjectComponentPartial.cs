using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
    .Include(p => p.Category)
    .Include(p => p.Skills)
    .Where(p => p.IsActive)                  // 🆕 sadece yayındakiler
    .OrderByDescending(p => p.ProjectId)     // 🆕 en yeni en başta
    .ToList();
            return View(values);
        }
    }
}
