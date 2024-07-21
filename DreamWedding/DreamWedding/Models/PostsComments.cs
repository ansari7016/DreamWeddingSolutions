using DreamWedding.Data;
using System.ComponentModel.DataAnnotations;

namespace DreamWedding.Models
{
    public class PostsComments
    {
        public int PostsCommentsId { get; set; }

        [StringLength(250)]
        [Required]
        public string Comments { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
