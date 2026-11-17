using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UpdateAbout()
        {
            // Kayıt varsa onu, yoksa boş bir form göster
            var value = _context.Abouts.FirstOrDefault() ?? new About();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAbout(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }

            var mevcut = _context.Abouts.FirstOrDefault();

            if (mevcut == null)
            {
                _context.Abouts.Add(about);   // ilk kez → oluştur
            }
            else
            {
                mevcut.NameSurname = about.NameSurname;
                mevcut.JobTitle = about.JobTitle;
                mevcut.Location = about.Location;
                mevcut.Expert = about.Expert;
                mevcut.Email = about.Email;
                mevcut.Description = about.Description;
                mevcut.OpenToWork = about.OpenToWork;
            }

            _context.SaveChanges();
            TempData["Success"] = "Hakkımda bilgileri kaydedildi.";
            return RedirectToAction("UpdateAbout");   // aynı sayfaya geri dön
        }
    }
}