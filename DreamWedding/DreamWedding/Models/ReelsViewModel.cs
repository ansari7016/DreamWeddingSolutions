using System.ComponentModel.DataAnnotations;

namespace DreamWedding.Models
{
    public class ReelsViewModel
    {
        [Required]
        public IFormFile VideoFile { get; set; }

        [StringLength(50)]
        public string Caption { get; set; }
    }
}
