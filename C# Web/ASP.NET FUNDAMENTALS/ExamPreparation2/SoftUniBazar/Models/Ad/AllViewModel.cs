using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Data.Constants.DataConstants.Ad;

namespace SoftUniBazar.Models.Ad
{
    public class AllViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLegth, MinimumLength = NameMinLegth)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLegth, MinimumLength = DescriptionMinLegth)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string Category { get; set; } = null!;
    }
}
