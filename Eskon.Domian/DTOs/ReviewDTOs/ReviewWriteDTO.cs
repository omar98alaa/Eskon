using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.ReviewDTOs
{
    public class ReviewWriteDTO
    {
        [Required, Range(0, 5)]
        public decimal Rating { get; set; }

        [Required, StringLength(500)]
        public string Content { get; set; }
    }
}
