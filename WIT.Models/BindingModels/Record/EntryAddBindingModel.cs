namespace WIT.Models.BindingModels.Record
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EntryAddBindingModel
    {
        [Required]
        public string EntryType { get; set; }

        [Required]
        [Display(Name = "Warehouse")]
        public string WhId { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "The amount can not be a negative number.")]
        public double Amount { get; set; }
    }
}
