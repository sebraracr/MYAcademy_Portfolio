using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class About
    {
        public int AboutId { get; set; }
        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Ad Soyad 3-60 karakter arasında olmalıdır.")]
        public string NameSurname { get; set; }
        [Required(ErrorMessage = "Meslek adı zorunludur.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Meslek adı 3-80 karakter arasında olmalıdır.")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Konum zorunludur.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Konum 3-60 karakter arasında olmalıdır.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Uzmanlık alanı 3-100 karakter arasında olmalıdır.")]
        public string Expert { get; set; }
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Açıklama alanı 20-1000 karakter arasında olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Çalışma durumu zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Çalışma durumu 3-100 karakter arasında olmalıdır.")]
        public string OpenToWork { get; set; }

    }
}
