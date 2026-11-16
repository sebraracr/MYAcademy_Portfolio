using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        // ── LİSTELE ──
        public IActionResult CategoryList()
        {
            var values = _context.Categories
                .Include(x => x.Projects)        // her kategoride kaç proje var, saymak için
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(values);
        }

        // ── EKLE ──
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            // 🆕 Aynı isimde kategori var mı?
            if (_context.Categories.Any(x => x.CategoryName == category.CategoryName))
            {
                ModelState.AddModelError(nameof(Category.CategoryName), "Bu isimde bir kategori zaten var.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori eklendi.";
            return RedirectToAction("CategoryList");
        }

        // ── GÜNCELLE ──
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("CategoryList");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(Category category)
        {
            // Aynı isimde BAŞKA bir kategori var mı? (kendisi hariç)
            if (_context.Categories.Any(x => x.CategoryName == category.CategoryName
                                          && x.CategoryId != category.CategoryId))
            {
                ModelState.AddModelError(nameof(Category.CategoryName), "Bu isimde bir kategori zaten var.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var value = _context.Categories.Find(category.CategoryId);
            if (value == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("CategoryList");
            }

            value.CategoryName = category.CategoryName;
            _context.SaveChanges();
            TempData["Success"] = "Kategori güncellendi.";
            return RedirectToAction("CategoryList");
        }

        // ── SİL (korumalı) ──
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("CategoryList");
            }

            // 🛡️ İçinde proje varsa SİLME. Cascade yüzünden projeler de silinirdi!
            var projeSayisi = _context.Projects.Count(x => x.CategoryId == id);
            if (projeSayisi > 0)
            {
                TempData["Error"] = $"Bu kategoride {projeSayisi} proje var. Önce projeleri Proje kategorisinden silin.";
                return RedirectToAction("CategoryList");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction("CategoryList");
        }
    }
}