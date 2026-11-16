using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Areas.Admin.Models;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;   // wwwroot'un yerini bilmek için

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ── LİSTELEME (zaten vardı) ──
        public IActionResult ProjectList()
        {
            var values = _context.Projects
                .Include(x => x.Category)
                .Include(x => x.Skills)
                .OrderByDescending(x => x.ProjectId)
                .ToList();
            return View(values);
        }

        // ⭐ ── EKLE: Sayfayı aç ──
        [HttpGet]
        public IActionResult CreateProject()
        {
            var model = new ProjectFormViewModel();
            ListeleriDoldur(model);          // dropdown seçeneklerini doldur
            return View(model);
        }

        // ⭐ ── EKLE: Kaydet'e basınca ──
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(ProjectFormViewModel model)
        {
            // 0) GÖRSEL KONTROLÜ: hatalıysa ModelState'e hata ekle
            if (model.CoverImage != null)
            {
                var gorselHatasi = GorselKontrol(model.CoverImage);
                if (gorselHatasi != null)
                {
                    ModelState.AddModelError(nameof(model.CoverImage), gorselHatasi);
                }
            }

            // 1) KONTROL: Hatalı alan varsa formu geri göster
            if (!ModelState.IsValid)
            {
                ListeleriDoldur(model);      // dropdown'lar boş kalmasın
                return View(model);
            }

            // 2) SEÇİLEN YETENEKLERİ GETİR: formdan Id'ler geliyor, örn. [3, 5, 8]
            var secilenYetenekler = _context.Skills
                .Where(s => model.SelectedSkillIds.Contains(s.SkillId))
                .ToList();

            // 3) ViewModel → Entity
            var project = new Project
            {
                Title = model.Title,
                Description = model.Description,
                GithubUrl = model.GithubUrl,
                IsActive = model.IsActive,
                CategoryId = model.CategoryId,
                Skills = secilenYetenekler
            };

            // 4) GÖRSEL: seçildiyse kaydet ve yolunu projeye yaz
            if (model.CoverImage != null)
            {
                project.CoverImageUrl = GorselKaydet(model.CoverImage);
            }
            // 5) KAYDET + BİLDİR + YÖNLENDİR
            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje eklendi.";
            return RedirectToAction("ProjectList");
        }

        // ⭐ ── YARDIMCI: Dropdown seçeneklerini doldur ──
        private void ListeleriDoldur(ProjectFormViewModel model)
        {
            // Kategoriler → <option value="2">Web</option>
            model.CategoryList = _context.Categories
                .OrderBy(x => x.CategoryName)
                .Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.CategoryName
                })
                .ToList();

            // Yetenekler → <option value="3">C#</option>
            model.SkillList = _context.Skills
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Value = x.SkillId.ToString(),
                    Text = x.Name
                })
                .ToList();
        }
        // Dosya uygun mu? Uygunsa null, değilse hata mesajı döner.
        private string? GorselKontrol(IFormFile dosya)
        {
            var izinliUzantilar = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var uzanti = Path.GetExtension(dosya.FileName).ToLowerInvariant();

            if (!izinliUzantilar.Contains(uzanti))
                return "Sadece JPG, PNG veya WEBP görsel yükleyebilirsiniz.";

            if (dosya.Length > 10 * 1024 * 1024)   // 10 MB
                return "Görsel en fazla 10  MB olabilir.";

            return null;
        }

        // Dosyayı wwwroot/images/projects içine benzersiz adla kaydeder, yolunu döner.
        private string GorselKaydet(IFormFile dosya)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "projects");
            Directory.CreateDirectory(klasor);   // klasör yoksa oluşturur

            var uzanti = Path.GetExtension(dosya.FileName).ToLowerInvariant();
            var yeniAd = Guid.NewGuid().ToString() + uzanti;
            var tamYol = Path.Combine(klasor, yeniAd);

            using (var stream = new FileStream(tamYol, FileMode.Create))
            {
                dosya.CopyTo(stream);
            }

            return "/images/projects/" + yeniAd;   // veritabanına yazılacak yol
        }
        // Eski görsel dosyasını siler (sadece bizim yüklediğimiz dosyaları)
        private void GorselSil(string? yol)
        {
            if (string.IsNullOrWhiteSpace(yol) || !yol.StartsWith("/images/projects/"))
                return;   // boşsa veya dışarıdan bir linkse dokunma

            var tamYol = Path.Combine(_env.WebRootPath, yol.TrimStart('/'));
            if (System.IO.File.Exists(tamYol))
            {
                System.IO.File.Delete(tamYol);
            }
        }
        // ── GÜNCELLE: Sayfayı aç (form dolu gelsin) ──
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var project = _context.Projects
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.ProjectId == id);

            if (project == null)
            {
                TempData["Error"] = "Proje bulunamadı.";
                return RedirectToAction("ProjectList");
            }

            // Entity → ViewModel
            var model = new ProjectFormViewModel
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
                Description = project.Description,
                GithubUrl = project.GithubUrl,
                IsActive = project.IsActive,
                CategoryId = project.CategoryId,
                CoverImageUrl = project.CoverImageUrl,
                SelectedSkillIds = project.Skills.Select(s => s.SkillId).ToList()   // işaretli gelecekler
            };

            ListeleriDoldur(model);
            return View(model);
        }
        // ── GÜNCELLE: Kaydet'e basınca ──
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProject(ProjectFormViewModel model)
        {
            // 0) Görsel kontrolü (Create'teki ile aynı)
            if (model.CoverImage != null)
            {
                var gorselHatasi = GorselKontrol(model.CoverImage);
                if (gorselHatasi != null)
                {
                    ModelState.AddModelError(nameof(model.CoverImage), gorselHatasi);
                }
            }

            // 1) KONTROL
            if (!ModelState.IsValid)
            {
                ListeleriDoldur(model);
                return View(model);
            }

            // 2) BUL
            var project = _context.Projects
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.ProjectId == model.ProjectId);

            if (project == null)
            {
                TempData["Error"] = "Proje bulunamadı.";
                return RedirectToAction("ProjectList");
            }

            // 3) ALANLARI GÜNCELLE
            project.Title = model.Title;
            project.Description = model.Description;
            project.GithubUrl = model.GithubUrl;
            project.IsActive = model.IsActive;
            project.CategoryId = model.CategoryId;

            // 4) YETENEKLER: fark mantığı
            //    Projede var ama artık seçili değil → bağlantıyı kopar
            var cikarilacaklar = project.Skills
                .Where(s => !model.SelectedSkillIds.Contains(s.SkillId))
                .ToList();
            foreach (var skill in cikarilacaklar)
            {
                project.Skills.Remove(skill);   // yeteneği SİLMEZ, sadece bu projeyle bağlantısını koparır
            }

            //    Yeni seçilmiş ama projede yok → bağla
            var mevcutIdler = project.Skills.Select(s => s.SkillId).ToList();
            var eklenecekler = _context.Skills
                .Where(s => model.SelectedSkillIds.Contains(s.SkillId) && !mevcutIdler.Contains(s.SkillId))
                .ToList();
            foreach (var skill in eklenecekler)
            {
                project.Skills.Add(skill);
            }

            // 5) GÖRSEL: yeni dosya seçildiyse eskisini sil, yenisini kaydet
            if (model.CoverImage != null)
            {
                GorselSil(project.CoverImageUrl);
                project.CoverImageUrl = GorselKaydet(model.CoverImage);
            }

            // 6) KAYDET + BİLDİR + YÖNLENDİR
            _context.SaveChanges();
            TempData["Success"] = "Proje güncellendi.";
            return RedirectToAction("ProjectList");
        }
        // ── SİL ──
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                TempData["Error"] = "Proje bulunamadı.";
                return RedirectToAction("ProjectList");
            }

            GorselSil(project.CoverImageUrl);      // 🆕 önce görsel dosyasını sil
            _context.Projects.Remove(project);     // sonra veritabanı kaydını
            _context.SaveChanges();

            TempData["Success"] = "Proje silindi.";
            return RedirectToAction("ProjectList");
        }

    }
}