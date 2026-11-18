using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultContactComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Contacts.FirstOrDefault() ?? new Contact();
            ViewBag.v1 = values.Description;
            ViewBag.v2 = values.Email;
            ViewBag.v3 = values.Linkedin;
            ViewBag.v4 = values.Location;
            ViewBag.v5 = values.Github;
            ViewBag.v6 = values.SubDescription;
            return View();
        }
    }
}
