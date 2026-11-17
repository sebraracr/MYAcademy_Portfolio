using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly PortfolioContext _context;

        public SocialMediaController(PortfolioContext context)
        {
            _context = context;
        }

        // ── LİSTELE ──
        public IActionResult SocialMediaList()
        {
            var values = _context.SocialMedias.OrderBy(x => x.SocialMediaId).ToList();
            return View(values);
        }

        // ── EKLE ──
        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            LinkKontrol(socialMedia.Url);

            if (!ModelState.IsValid)
            {
                return View(socialMedia);
            }

            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            TempData["Success"] = "Bağlantı eklendi.";
            return RedirectToAction("SocialMediaList");
        }

        // ── GÜNCELLE ──
        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Bağlantı bulunamadı.";
                return RedirectToAction("SocialMediaList");
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            LinkKontrol(socialMedia.Url);

            if (!ModelState.IsValid)
            {
                return View(socialMedia);
            }

            var value = _context.SocialMedias.Find(socialMedia.SocialMediaId);
            if (value == null)
            {
                TempData["Error"] = "Bağlantı bulunamadı.";
                return RedirectToAction("SocialMediaList");
            }

            value.Name = socialMedia.Name;
            value.Url = socialMedia.Url;
            value.Icon = socialMedia.Icon;
            _context.SaveChanges();
            TempData["Success"] = "Bağlantı güncellendi.";
            return RedirectToAction("SocialMediaList");
        }

        // ── SİL ──
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                _context.SocialMedias.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Bağlantı silindi.";
            }
            return RedirectToAction("SocialMediaList");
        }

        // 🆕 Link http://, https:// veya mailto: ile başlamalı
        private void LinkKontrol(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;   // boşsa [Required] zaten yakalar

            if (!url.StartsWith("http://") && !url.StartsWith("https://") && !url.StartsWith("mailto:"))
            {
                ModelState.AddModelError(nameof(SocialMedia.Url),
                    "Link https:// veya mailto: ile başlamalıdır.");
            }
        }
    }
}