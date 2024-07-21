using DreamWedding.Data;

namespace DreamWedding.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<PostsComments> PostsComments { get; set; }
        public ICollection<PostsLike> Likes { get; set; } = new List<PostsLike>();
        public ICollection<SavePosts> SavePosts { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
