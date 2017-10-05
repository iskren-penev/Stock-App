namespace WIT.Models.ViewModels.Record
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RecordListViewModel
    {
        [Required]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string EntryType { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double Amount { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double AmountBefore { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double AmountAfter { get; set; }
    }
}
