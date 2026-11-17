using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        [Required(ErrorMessage = "Sosyal medya adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Sosyal medya adı en fazla 50 karakter olmalıdır.")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Link zorunludur.")]
        [StringLength(250, ErrorMessage = "Link en fazla 250 karakter olabilir.")]
        public string Url { get; set; }
        [Required(ErrorMessage = "İkon zorunludur.")]
        [StringLength(50, ErrorMessage = "Icon en fazla 50 karakter olmalıdır.")]

        public string Icon { get; set; }
    }
}
