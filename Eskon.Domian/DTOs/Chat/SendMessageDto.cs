using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.Chat
{
    public class SendMessageDto
    {
        [Required]
        public Guid ChatId { get; set; }

        [Required, StringLength(500)]
        public string Content { get; set; }
    }

}
