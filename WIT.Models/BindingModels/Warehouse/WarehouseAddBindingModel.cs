namespace WIT.Models.BindingModels.Warehouse
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WarehouseAddBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and 50 characters long.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The {0} must be between {2} and 100 characters long.")]
        public string Address { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double Capacity { get; set; }
    }
}
