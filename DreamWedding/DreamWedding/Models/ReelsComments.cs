using DreamWedding.Data;
using System.ComponentModel.DataAnnotations;

namespace DreamWedding.Models
{
    public class ReelsComments
    {
        public int ReelsCommentsId { get; set; }

        [StringLength(250)]
        [Required]
        public string Comments { get; set; }

        public int ReelsId { get; set; }
        public Reels Reels { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
