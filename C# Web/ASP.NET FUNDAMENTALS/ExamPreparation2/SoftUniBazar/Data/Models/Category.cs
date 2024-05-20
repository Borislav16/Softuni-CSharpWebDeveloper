using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Constants.DataConstants.Category;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Ad> Ads { get; set; } = new List<Ad>();
    }
}
