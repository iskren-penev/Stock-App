namespace Stock.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public string Content { get; set; }
        
        public virtual User User { get; set; }
    }
}
