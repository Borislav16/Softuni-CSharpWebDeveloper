using System.ComponentModel.DataAnnotations;
using static ForumApp.Data.Constants.DataConstants.Post;
namespace ForumApp.Models.Post
{
    public class PostFormModel
    {

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]

        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = string.Empty;
    }
}
