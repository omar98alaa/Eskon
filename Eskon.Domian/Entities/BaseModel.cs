using Eskon.Domian.Entities;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.Models
{
    public class BaseModel : IBaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
