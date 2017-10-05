namespace WIT.Models.ViewModels.Warehouse
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EntryAddViewModel
    {
        [Required]
        public string EntryType { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "The amount can not be a negative number.")]
        public double Amount { get; set; }
    }
}
