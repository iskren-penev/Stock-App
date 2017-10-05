namespace WIT.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StockEntry
    {
        [Key, ForeignKey("Record")]
        public int Id { get; set; }

        [Required]
        public string EntryType { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime? EntryDate { get; set; }

        [ForeignKey("Warehouse")]
        public int? WarehouseId { get; set; }

        public virtual Warehouse Warehouse { get; set; }


        public virtual Record Record { get; set; }
    }
}
