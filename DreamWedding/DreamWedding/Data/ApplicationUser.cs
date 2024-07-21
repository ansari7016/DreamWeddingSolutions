using Microsoft.AspNetCore.Identity;
using DreamWedding.Models;

namespace DreamWedding.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<PostsComments> PostsComments { get; set; }
        public ICollection<PostsLike> Likes { get; set; } = new List<PostsLike>();
        public ICollection<ReelsComments> ReelsComments { get; set; }
        public ICollection<ReelsLikes> ReelsLikes { get; set; } = new List<ReelsLikes>();
        public ICollection<SavePosts> SavePosts { get; set; }
    }
}
