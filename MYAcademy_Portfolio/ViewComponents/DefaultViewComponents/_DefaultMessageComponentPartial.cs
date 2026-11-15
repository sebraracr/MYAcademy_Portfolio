using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultMessageComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
