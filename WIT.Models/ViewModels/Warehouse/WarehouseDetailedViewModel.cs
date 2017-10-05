namespace WIT.Models.ViewModels.Warehouse
{
    using System.Collections.Generic;

    public class WarehouseDetailedViewModel
    {
        public WarehouseListViewModel Info { get; set; }

        public IEnumerable<EntryListViewModel> Entries { get; set; }
    }
}
