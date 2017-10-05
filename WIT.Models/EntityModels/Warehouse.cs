namespace WIT.Models.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Warehouse
    {
        private ICollection<StockEntry> entries;

        public Warehouse()
        {
            this.entries = new HashSet<StockEntry>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and 50 characters long.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and 100 characters long.")]
        public string Address { get; set; }

        [Range(0, Double.MaxValue)]
        public double CurrentStock { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double Capacity { get; set; }

        public virtual ICollection<StockEntry> Entries
        {
            get { return this.entries; }
            set { this.entries = value; }
        }
    }
}
