using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;   
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(Message model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ContactError"] = "Lütfen tüm alanları doğru şekilde doldurunuz.";
                return RedirectToAction("Index", "Default", null, "contact");
            }

            model.SendDate = DateTime.Now;
            model.IsRead = false;

            _context.Messages.Add(model);
            await _context.SaveChangesAsync();

            TempData["ContactSuccess"] = "Mesajınız başarıyla gönderildi.";
            return RedirectToAction("Index", "Default", null, "contact");
        }
    }
}