using DreamWedding.Data;

namespace DreamWedding.Models
{
    public class ReelsLikes
    {
        public int ReelsLikesId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ReelsId { get; set; }
        public Reels Reels { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
