using DreamWedding.Data;

namespace DreamWedding.Models
{
    public class PostsLike
    {
        public int LikeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
