using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DreamWedding.Data;

namespace DreamWedding.Models
{
    public class Chats
    {
        [Key]
        public int ChatsId { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; }
    }
}
