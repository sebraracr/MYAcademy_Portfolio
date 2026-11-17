using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Abouts.FirstOrDefault() ?? new About();
            ViewBag.v1 = values.NameSurname;
            ViewBag.v2 = values.JobTitle;
            ViewBag.v3 = values.Location;
            ViewBag.v4 = values.Expert;
            ViewBag.v5 = values.Email;
            ViewBag.v6 = values.OpenToWork;
            ViewBag.v7 = values.Description;

            return View();
        }
    }
}
