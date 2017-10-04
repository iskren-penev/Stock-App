namespace Stock.Services.Interfaces
{
    using System.Collections.Generic;
    using Stock.Models.BindingModels.Warehouse;
    using Stock.Models.ViewModels.Warehouse;

    public interface IWarehouseService : IService
    {

        IEnumerable<WarehouseListViewModel> GetListViewModelsSearch(string search);

        void AddWarehouse(WarehouseAddBindingModel model);

        void EditWarehouse(WarehouseEditBindingModel model);

        WarehouseDetailedViewModel GetDetailedViewModel(int id);

        WarehouseAddViewModel GetAddViewModel(WarehouseAddBindingModel model);

        WarehouseEditViewModel GetEditViewModel(int id);
    }
}
