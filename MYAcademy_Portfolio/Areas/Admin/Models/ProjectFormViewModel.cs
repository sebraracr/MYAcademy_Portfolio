using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Areas.Admin.Models
{
    public class ProjectFormViewModel
    {
        public int ProjectId { get; set; }

        // ── Bildiğin alanlar (Project.cs'den) ──
        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Proje adı 3-100 karakter arasında olmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Proje açıklaması zorunludur.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Proje açıklaması 3-300 karakter arasında olmalıdır.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Geçerli bir link giriniz (https://... ile başlamalı).")]
        public string? GithubUrl { get; set; }

        public bool IsActive { get; set; } = true;

        // ── 1) Tek seçimli dropdown: Kategori ──
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public List<SelectListItem> CategoryList { get; set; } = new();

        // ── 2) Çoklu seçim: Yetenekler ──
        public List<int> SelectedSkillIds { get; set; } = new();

        [ValidateNever]
        public List<SelectListItem> SkillList { get; set; } = new();

        // ── 3) Dosya yükleme: Kapak görseli ──
        public IFormFile? CoverImage { get; set; }
        public string? CoverImageUrl { get; set; }
    }
}