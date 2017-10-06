namespace WIT.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class CommentAddViewModel
    {
        [Required]
        [Display(Name = "Company")]
        public string UserId { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "The {0} must be between {2} and 5000 characters long.")]
        public string Content { get; set; }
    }
}
