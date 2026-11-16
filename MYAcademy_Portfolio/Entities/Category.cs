using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Kategori adı 2-50 karakter arasında olmalıdır.")]
        public string CategoryName { get; set; }
        [ValidateNever]
        public List<Project> Projects { get; set; }
    }
}
