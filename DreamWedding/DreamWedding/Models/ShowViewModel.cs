namespace DreamWedding.Models
{
    public class ShowViewModel
    {
        public IEnumerable<DreamWedding.Models.Advertisment> Advertisements { get; set; }
        public IEnumerable<DreamWedding.Models.Post> Posts { get; set; }
        public IEnumerable<DreamWedding.Data.ApplicationUser> Users { get; set; }
        public IEnumerable<DreamWedding.Models.Chats> Chats { get; set; }
        public IEnumerable<DreamWedding.Models.Reels> Reels { get; set; }
    }
}
