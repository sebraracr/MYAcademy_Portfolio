using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class SkillCategory
    {
        public int SkillCategoryId { get; set; }
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Kategori adı 3-100 karakter arasında olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kategori açıklaması zorunludur.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Kategori açıklaması 3-300 karakter arasında olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Simge zorunludur.")]
        [StringLength(50, ErrorMessage = "Simge en fazla 50 karakter olmalıdır.")]
        public string Icon { get; set; }
        [ValidateNever]
        public List<Skill> Skills { get; set; }

    }
}
