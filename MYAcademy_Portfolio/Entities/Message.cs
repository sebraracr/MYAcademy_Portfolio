using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        public string NameSurname { get; set; }

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]
        public string Subject { get; set; }

        [Required, StringLength(2000, MinimumLength = 10)]
        public string MessageDetail { get; set; }

        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}