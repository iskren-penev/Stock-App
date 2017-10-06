namespace WIT.Models.BindingModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class CommentAddBindingModel
    {
        public string UserId { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public string Content { get; set; }
    }
}
