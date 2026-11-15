using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Experiences
    .Include(x => x.Skills)
    .Where(x => x.IsActive)
    .AsEnumerable()
    .OrderByDescending(x => System.Text.RegularExpressions.Regex
        .Match(x.Duration ?? "", @"\b\d{4}\b").Value)
    .ToList();

            return View(values);


        }
    }
}
