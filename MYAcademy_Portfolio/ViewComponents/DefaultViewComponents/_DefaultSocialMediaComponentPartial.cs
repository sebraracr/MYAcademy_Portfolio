using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSocialMediaComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSocialMediaComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.SocialMedias.OrderBy(x => x.SocialMediaId).ToList();
            return View(values);
        }
    }
}