using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HeroSection hero, IFormFile? profileImage, IFormFile? cvFile)
        {
            // 1) DOSYA KONTROLLERİ
            if (profileImage != null)
            {
                var uzanti = Path.GetExtension(profileImage.FileName).ToLowerInvariant();
                if (!new[] { ".jpg", ".jpeg", ".png", ".webp" }.Contains(uzanti))
                    ModelState.AddModelError("profileImage", "Profil fotoğrafı JPG, PNG veya WEBP olmalıdır.");
                else if (profileImage.Length > 5 * 1024 * 1024)
                    ModelState.AddModelError("profileImage", "Profil fotoğrafı en fazla 5 MB olabilir.");
            }

            if (cvFile != null)
            {
                var uzanti = Path.GetExtension(cvFile.FileName).ToLowerInvariant();
                if (uzanti != ".pdf" || !GercektenPdfMi(cvFile))
                    ModelState.AddModelError("cvFile", "CV yalnızca PDF formatında olabilir.");
                else if (cvFile.Length > 10 * 1024 * 1024)
                    ModelState.AddModelError("cvFile", "CV en fazla 10 MB olabilir.");
            }

            var mevcut = _context.HeroSections.FirstOrDefault();

            // 2) KONTROL: hata varsa formu geri göster (mevcut fotoğraf ve CV kaybolmasın)
            if (!ModelState.IsValid)
            {
                hero.ImageUrl = mevcut?.ImageUrl;
                hero.CvUrl = mevcut?.CvUrl;
                return View(hero);
            }

            // 3) VARSA GÜNCELLE, YOKSA OLUŞTUR
            if (mevcut == null)
            {
                mevcut = new HeroSection();
                _context.HeroSections.Add(mevcut);
            }

            mevcut.Title = hero.Title;
            mevcut.SubTitle = hero.SubTitle;
            mevcut.Description = hero.Description;

            // 4) DOSYALAR: yeni seçildiyse eskisini sil, yenisini kaydet
            if (profileImage != null)
            {
                DosyaSil(mevcut.ImageUrl, "/images/profile/");
                mevcut.ImageUrl = DosyaKaydet(profileImage, "images/profile");
            }

            if (cvFile != null)
            {
                DosyaSil(mevcut.CvUrl, "/cv/");
                mevcut.CvUrl = DosyaKaydet(cvFile, "cv");
            }

            _context.SaveChanges();
            TempData["Success"] = "Ayarlar kaydedildi.";
            return RedirectToAction("Index");
        }

        // ── YARDIMCILAR ──

        // Dosya gerçekten PDF mi? (ilk 4 byte "%PDF" olmalı)
        private bool GercektenPdfMi(IFormFile dosya)
        {
            using var stream = dosya.OpenReadStream();
            var ilkBytelar = new byte[4];
            stream.Read(ilkBytelar, 0, 4);
            return System.Text.Encoding.ASCII.GetString(ilkBytelar) == "%PDF";
        }

        // wwwroot/{klasor} içine benzersiz adla kaydeder, yolunu döner
        private string DosyaKaydet(IFormFile dosya, string klasor)
        {
            var hedef = Path.Combine(_env.WebRootPath, klasor);
            Directory.CreateDirectory(hedef);

            var yeniAd = Guid.NewGuid() + Path.GetExtension(dosya.FileName).ToLowerInvariant();
            using (var stream = new FileStream(Path.Combine(hedef, yeniAd), FileMode.Create))
            {
                dosya.CopyTo(stream);
            }
            return "/" + klasor + "/" + yeniAd;
        }

        // Sadece belirtilen klasördeki dosyayı siler (güvenlik)
        private void DosyaSil(string? yol, string izinliKlasor)
        {
            if (string.IsNullOrWhiteSpace(yol) || !yol.StartsWith(izinliKlasor)) return;

            var tamYol = Path.Combine(_env.WebRootPath, yol.TrimStart('/'));
            if (System.IO.File.Exists(tamYol))
                System.IO.File.Delete(tamYol);
        }
    }
}