namespace WIT.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using WIT.Models.BindingModels.Record;
    using WIT.Models.ViewModels.Record;

    public interface IRecordService : IService
    {
        void AddEntry(EntryAddBindingModel model, string userId);

        EntryAddViewModel GetEntryAddViewModel(EntryAddBindingModel model);

        IEnumerable<SelectListItem> GetWarehousesSelectListItems();
    }
}
