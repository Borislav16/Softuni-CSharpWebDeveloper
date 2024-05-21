using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models.Category
{
    public class CategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}