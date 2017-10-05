namespace WIT.Models.ViewModels.Warehouse
{
    using System.ComponentModel.DataAnnotations;

    public class WarehouseListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "Current Quantity (tons)")]
        public double? CurrentStock { get; set; }

        [Display(Name = "Capacity (tons)")]
        public double? Capacity { get; set; }
    }
}
