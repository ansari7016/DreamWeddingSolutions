using Microsoft.EntityFrameworkCore.Diagnostics;
using DreamWedding.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamWedding.Models
{
    public class Reels
    {
        public int ReelsId { get; set; }

        [Required]
        public string ReelsVideo { get; set; }

        [StringLength(50)]
        public string Captions { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ReelsComments> ReelsComments { get; set; }
        public ICollection<ReelsLikes> ReelsLikes { get; set; } = new List<ReelsLikes>();

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
