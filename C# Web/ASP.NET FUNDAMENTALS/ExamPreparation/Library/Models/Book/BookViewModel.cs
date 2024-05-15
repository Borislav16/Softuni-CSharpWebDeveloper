using Library.Data.Models;
using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;
namespace Library.Models.Book
{
    public class BookViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [Range(0.00, 10.00)]
        public decimal Rating { get; set; }

        [Required]
        public string Category { get; set; } = null!;
    }
}
