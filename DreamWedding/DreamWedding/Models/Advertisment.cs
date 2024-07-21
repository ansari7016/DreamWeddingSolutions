using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamWedding.Models
{
    public class Advertisment
    {
        public int AdvertismentId { get; set; }

        [Required]
        [DisplayName("Advertisment")]
        [StringLength(1000)]
        public string AdsImage { get; set; }

        [Required]
        [StringLength(100)]
        public string AdsLink { get; set; }
    }
}
