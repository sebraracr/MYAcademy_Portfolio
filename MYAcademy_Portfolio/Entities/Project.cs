using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Proje adı 3-100 karakter arasında olmalıdır.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Proje açıklaması zorunludur.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Proje açıklaması 3-300 karakter arasında olmalıdır.")]
        public string Description { get; set; }
        [Url(ErrorMessage = "Geçerli bir link giriniz (https://... ile başlamalı).")]
        public string? GithubUrl { get; set; }
        [ValidateNever]
        public string? CoverImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public List<Skill> Skills { get; set; }

    }
}
