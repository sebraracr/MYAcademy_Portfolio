using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultNavbarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Ayarlar'da yüklenen CV'nin yolu (yoksa null)
            ViewBag.CvUrl = _context.HeroSections
                .Select(x => x.CvUrl)
                .FirstOrDefault();

            return View();
        }
    }
}