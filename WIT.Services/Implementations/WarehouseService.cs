namespace WIT.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using WIT.Data.Interfaces;
    using WIT.Models.BindingModels.Warehouse;
    using WIT.Models.EntityModels;
    using WIT.Models.ViewModels.Warehouse;
    using WIT.Services.Interfaces;

    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(IWitContext context) : base(context)
        {
        }

        public List<WarehouseListViewModel> GetListViewModels()
        {
            List<Warehouse> warehouses = this.GetWarehouses().ToList();
            List<WarehouseListViewModel> viewModels = Mapper.Instance
                .Map<List<Warehouse>, List<WarehouseListViewModel>>(warehouses);

            return viewModels;
        }

        public List<WarehouseListViewModel> SearchWarehouses(string search)
        {
            List<WarehouseListViewModel> viewModels = this.GetListViewModels();

            if (!string.IsNullOrEmpty(search))
            {
                viewModels = viewModels.Where(wh =>
                    wh.Address.ToLower().Contains(search.ToLower())
                    || wh.Name.ToLower().Contains(search.ToLower()))
                    .ToList();
            }

            return viewModels;
        }

        public void AddWarehouse(WarehouseAddBindingModel model)
        {
            Warehouse warehouse = Mapper.Instance
                .Map<WarehouseAddBindingModel, Warehouse>(model);
            warehouse.CurrentStock = 0;

            this.Context.Warehouses.Add(warehouse);
            this.Context.SaveChanges();
        }

        public void EditWarehouse(WarehouseEditBindingModel model)
        {
            Warehouse warehouse = this.GetWarehouseById(model.Id);
            warehouse.Name = model.Name;
            warehouse.Address = model.Address;
            if (model.Capacity < warehouse.CurrentStock)
            {
                throw new ArgumentException("The amount of stock in the warehouse cannot exceed the capacity!");
            }
            warehouse.Capacity = model.Capacity;

            this.Context.SaveChanges();
        }

        public WarehouseDetailedViewModel GetDetailedViewModel(int id)
        {
            Warehouse warehouse = this.GetWarehouseById(id);

            WarehouseDetailedViewModel viewModel = new WarehouseDetailedViewModel()
            {
                Info = Mapper.Instance
                     .Map<Warehouse,WarehouseListViewModel>(warehouse),
                Entries = Mapper.Instance
                     .Map<IEnumerable<StockEntry>,IEnumerable<EntryListViewModel>>(warehouse.Entries)
            };

            return viewModel;
        }

        public WarehouseAddViewModel GetAddViewModel(WarehouseAddBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            WarehouseAddViewModel viewModel = Mapper.Instance
                .Map<WarehouseAddBindingModel,WarehouseAddViewModel>(model);

            return viewModel;
        }

        public WarehouseEditViewModel GetEditViewModel(int id)
        {
            Warehouse warehouse = this.GetWarehouseById(id);
            WarehouseEditViewModel viewModel = Mapper.Instance
                .Map<Warehouse,WarehouseEditViewModel>(warehouse);

            return viewModel;
        }
    }
}
