namespace WIT.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Record
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double AmountBefore { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double AmountAfter { get; set; }
        
        [ForeignKey("StockEntry")]
        public int? StockEntryId { get; set; }

        public virtual StockEntry StockEntry { get; set; }

        public virtual User Distributor { get; set; }
    }
}
