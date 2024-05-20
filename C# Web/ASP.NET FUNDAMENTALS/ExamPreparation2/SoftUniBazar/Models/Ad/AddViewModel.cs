using SoftUniBazar.Data.Models;
using SoftUniBazar.Models.Category;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Constants.DataConstants.Ad;

namespace SoftUniBazar.Models.Ad
{
    public class AddViewModel
    {


        [Required]
        [StringLength(NameMaxLegth, MinimumLength = NameMinLegth)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLegth, MinimumLength = DescriptionMinLegth)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
