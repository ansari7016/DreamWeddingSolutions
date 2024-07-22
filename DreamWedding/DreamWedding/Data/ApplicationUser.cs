using Microsoft.AspNetCore.Identity;
using DreamWedding.Models;
using System.ComponentModel.DataAnnotations;

namespace DreamWedding.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
        public string Location { get; set; }
        [Required]
        [Display(Name = "Services Offered")]
        public string ServicesOffered { get; set; } 
        [Required]
        [Display(Name = "PriceRange")]
        public int PriceRange { get; set; }
        [Required]
        [Display(Name = "Reviews")]
        public string Reviews { get; set; }
        [Required]
        [Display(Name = "Gallery")]
        public string Gallery { get; set; }
        [Required]
        [Display(Name = "ContactDetails")]
        public int ContactDetails { get; set; }
        [Required]
        [Display(Name = "TravelCosts")]
        public int TravelCosts { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostsComments> PostsComments { get; set; }
        public ICollection<PostsLike> Likes { get; set; } = new List<PostsLike>();
        public ICollection<ReelsComments> ReelsComments { get; set; }
        public ICollection<ReelsLikes> ReelsLikes { get; set; } = new List<ReelsLikes>();
        public ICollection<SavePosts> SavePosts { get; set; }
    }
}
