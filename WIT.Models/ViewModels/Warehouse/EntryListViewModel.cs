namespace WIT.Models.ViewModels.Warehouse
{
    using System;

    public class EntryListViewModel
    {
        public string EntryType { get; set; }
        
        public double? Amount { get; set; }
        
        public DateTime? EntryDate { get; set; }
    }
}
