using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Başlık 2-60 karakter arasında olmalıdır.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Alt Başlık zorunludur.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Alt Başlık 2-80 karakter arasında olmalıdır.")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Açıklama 20-500 karakter arasında olmalıdır.")]
        public string Description { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        [ValidateNever]
        public string? CvUrl { get; set; }
    }
}
