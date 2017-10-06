namespace WIT.Services.Interfaces
{
    using System.Collections.Generic;
    using WIT.Models.BindingModels.Warehouse;
    using WIT.Models.ViewModels.Warehouse;

    public interface IWarehouseService : IService
    {
        List<WarehouseListViewModel> GetListViewModels();

        List<WarehouseListViewModel> SearchWarehouses(string search);

        void AddWarehouse(WarehouseAddBindingModel model);

        void EditWarehouse(WarehouseEditBindingModel model);

        WarehouseDetailedViewModel GetDetailedViewModel(int id);

        WarehouseAddViewModel GetAddViewModel(WarehouseAddBindingModel model);

        WarehouseEditViewModel GetEditViewModel(int id);
    }
}
