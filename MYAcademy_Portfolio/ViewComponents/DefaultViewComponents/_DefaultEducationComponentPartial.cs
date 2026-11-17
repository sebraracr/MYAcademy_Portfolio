using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Helpers;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultEducationComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Educations.Where(x => x.IsActive).ToList().OrderByDescending(x => DurationHelper.EndYear(x.Duration)).ThenByDescending(x => DurationHelper.StartYear(x.Duration)).ToList();
            return View(values);
        }
    }
}
