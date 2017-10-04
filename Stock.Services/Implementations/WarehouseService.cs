namespace Stock.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Stock.Data.Interfaces;
    using Stock.Models.BindingModels.Warehouse;
    using Stock.Models.EntityModels;
    using Stock.Models.ViewModels.Warehouse;
    using Stock.Services.Interfaces;
    using AutoMapper;

    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(IStockContext context) : base(context)
        {
        }
        
        public IEnumerable<WarehouseListViewModel> GetListViewModelsSearch(string search)
        {
            IEnumerable<Warehouse> warehouses = this.GetWarehouses()
                .Where(wh =>
                    wh.Address.Contains(search) 
                    || wh.Name.Contains(search));
            IEnumerable<WarehouseListViewModel> viewModels =
                Mapper.Instance.Map<IEnumerable<WarehouseListViewModel>>(warehouses);

            return viewModels;
        }

        public void AddWarehouse(WarehouseAddBindingModel model)
        {
            Warehouse warehouse = Mapper.Instance.Map<Warehouse>(model);
            warehouse.CurrentStock = 0;

            this.Context.Warehouses.Add(warehouse);
            this.Context.SaveChanges();
        }

        public void EditWarehouse(WarehouseEditBindingModel model)
        {
            Warehouse warehouse = this.GetWarehouseById(model.Id);
            warehouse.Name = model.Name;
            warehouse.Address = model.Name;
            if (model.Capacity > warehouse.CurrentStock)
            {
                warehouse.Capacity = model.Capacity;
            }

            this.Context.SaveChanges();
        }

        public WarehouseDetailedViewModel GetDetailedViewModel(int id)
        {
            Warehouse warehouse = this.GetWarehouseById(id);
            WarehouseDetailedViewModel viewModel = Mapper.Instance.Map<WarehouseDetailedViewModel>(warehouse);

            return viewModel;
        }

        public WarehouseAddViewModel GetAddViewModel(WarehouseAddBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            WarehouseAddViewModel viewModel = Mapper.Instance.Map<WarehouseAddViewModel>(model);

            return viewModel;
        }

        public WarehouseEditViewModel GetEditViewModel(int id)
        {
            Warehouse warehouse = this.GetWarehouseById(id);
            WarehouseEditViewModel viewModel = Mapper.Instance.Map<WarehouseEditViewModel>(warehouse);

            return viewModel;
        }
        

        private IEnumerable<Warehouse> GetWarehouses()
        {
            IEnumerable<Warehouse> warehouses = this.Context.Warehouses;

            return warehouses;
        }

        private Warehouse GetWarehouseById(int id)
        {
            Warehouse warehouse = this.Context.Warehouses.Find(id);
            if (warehouse == null)
            {
                throw new ArgumentNullException(nameof(id), "There is no Warehouse with such Id.");
            }

            return warehouse;
        }
    }
}
