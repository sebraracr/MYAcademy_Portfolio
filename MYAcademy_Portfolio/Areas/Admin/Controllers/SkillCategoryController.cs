using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;
using MYAcademy_Portfolio.Areas.Admin.Models;
namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillCategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillCategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillCategoryList()
        {
            var values = _context.SkillCategories
                .Include(x => x.Skills)
                .ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateSkillCategory()
        {
            return View(new SkillCategoryFormViewModel
            {
                SuggestedSkills = OnerilenYetenekler()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkillCategory(SkillCategoryFormViewModel model)
        {
            // 1) KONTROL: Kurallara uymayan alan varsa formu hatalarla geri göster
            if (!ModelState.IsValid)
            {
                model.SuggestedSkills = OnerilenYetenekler();
                return View(model);
            }

            // 2) TEMİZLE: Boşları at, boşlukları kırp, tekrarları sil
            var temizAdlar = model.SkillNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // 3) KATEGORİYİ OLUŞTUR: ViewModel'den Entity'ye aktar
            var skillCategory = new SkillCategory
            {
                Name = model.Name,
                Description = model.Description,
                Icon = model.Icon,
                Skills = new List<Skill>()
            };

            // 4) YETENEKLERİ EKLE: Temizlenmiş her ad için yeni bir Skill
            foreach (var ad in temizAdlar)
            {
                skillCategory.Skills.Add(new Skill { Name = ad });
            }

            // 5) KAYDET: EF önce kategoriyi kaydeder, sonra yeteneklere SkillCategoryId'yi kendisi yazar
            _context.SkillCategories.Add(skillCategory);
            _context.SaveChanges();

            // 6) BİLDİR VE YÖNLENDİR
            TempData["Success"] = "Kategori eklendi.";
            return RedirectToAction("SkillCategoryList");
        }
        private List<string> OnerilenYetenekler()
        {
            var hazirListe = new List<string>
    {
        "C#", "ASP.NET Core MVC", "ASP.NET Web API", "Entity Framework Core", "LINQ",
        "SQL", "Microsoft SQL Server", "PostgreSQL", "MongoDB", "Redis",
        "HTML5", "CSS3", "JavaScript", "TypeScript", "Bootstrap", "React", "Angular", "Vue.js",
        "Python", "Java", "Node.js", "Docker", "Git", "GitHub", "Postman", "Visual Studio",
        "Machine Learning", "Deep Learning", "OpenCV", "Razor", "Blazor", "Azure", "Linux"
    };

            // Veritabanında zaten olan yetenekler önerilmesin
            var mevcutlar = _context.Skills.Select(x => x.Name).ToList();

            return hazirListe
                .Where(x => !mevcutlar.Contains(x, StringComparer.OrdinalIgnoreCase))
                .ToList();
        }
        public IActionResult DeleteSkillCategory(int id)
        {
            var category = _context.SkillCategories
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.SkillCategoryId == id);
            if (category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("SkillCategoryList");
            }
            // Önce ilişkili yetenekleri sil
            _context.Skills.RemoveRange(category.Skills);
            // Sonra kategoriyi sil
            _context.SkillCategories.Remove(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori ve ilişkili yetenekler silindi.";
            return RedirectToAction("SkillCategoryList");
        }

        [HttpGet]
        public IActionResult UpdateSkillCategory(int id)
        {
            var category = _context.SkillCategories
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.SkillCategoryId == id);
            if (category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("SkillCategoryList");
            }
            var model = new SkillCategoryFormViewModel
            {
                SkillCategoryId = category.SkillCategoryId,
                Name = category.Name,
                Description = category.Description,
                Icon = category.Icon,
                SkillNames = category.Skills.Select(s => s.Name).ToList(),
                SuggestedSkills = OnerilenYetenekler()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkillCategory(SkillCategoryFormViewModel model)
        {
            // 1) KONTROL: Hatalı alan varsa formu geri göster
            if (!ModelState.IsValid)
            {
                model.SuggestedSkills = OnerilenYetenekler();
                return View(model);
            }

            // 2) BUL: Güncellenecek kategoriyi yetenekleriyle getir
            var category = _context.SkillCategories
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.SkillCategoryId == model.SkillCategoryId);

            if (category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("SkillCategoryList");
            }

            // 3) ALANLARI GÜNCELLE
            category.Name = model.Name;
            category.Description = model.Description;
            category.Icon = model.Icon;

            // 4) TEMİZLE: Boşları at, kırp, tekrarları sil
            var temizAdlar = model.SkillNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // 5) SİLİNECEKLER: Veritabanında var ama formda yok
            var silinecekler = category.Skills
                .Where(s => !temizAdlar.Contains(s.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();

            // 6) EKLENECEKLER: Formda var ama veritabanında yok
            var eklenecekler = temizAdlar
                .Where(ad => !category.Skills.Any(s => s.Name.Equals(ad, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            // 7) FARKI UYGULA: Dokunulmayan yetenekler (Id'leri ve proje bağlantıları) olduğu gibi kalır
            _context.Skills.RemoveRange(silinecekler);

            foreach (var ad in eklenecekler)
            {
                category.Skills.Add(new Skill { Name = ad });
            }

            // 8) KAYDET + BİLDİR + YÖNLENDİR
            _context.SaveChanges();
            TempData["Success"] = "Kategori güncellendi.";
            return RedirectToAction("SkillCategoryList");
        }
    }
}