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
            List<WarehouseListViewModel> viewModels =
                Mapper.Instance.Map<List<WarehouseListViewModel>>(warehouses);

            return viewModels;
        }

        public List<WarehouseListViewModel> GetListViewModelsSearch(string search)
        {
            List<Warehouse> warehouses = this.GetWarehouses().ToList();

            if (!string.IsNullOrEmpty(search))
            {
                warehouses = warehouses.Where(wh =>
                    wh.Address.ToLower().Contains(search)
                    || wh.Name.ToLower().Contains(search))
                    .ToList();
            }
            List<WarehouseListViewModel> viewModels =
                 Mapper.Instance.Map<List<WarehouseListViewModel>>(warehouses);

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
            warehouse.Address = model.Address;
            if (model.Capacity > warehouse.CurrentStock)
            {
                warehouse.Capacity = model.Capacity;
            }

            this.Context.SaveChanges();
        }

        public WarehouseDetailedViewModel GetDetailedViewModel(int id)
        {
            Warehouse warehouse = this.GetWarehouseById(id);

            WarehouseDetailedViewModel viewModel = new WarehouseDetailedViewModel()
            {
                Info = Mapper.Instance.Map<WarehouseListViewModel>(warehouse),
                Entries = Mapper.Instance.Map<IEnumerable<EntryListViewModel>>(warehouse.Entries)
            };

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

        public void AddEntry(EntryAddBindingModel model, int warehouseId, string userId)
        {
            Warehouse warehouse = this.GetWarehouseById(warehouseId);
            StockEntry entry = Mapper.Instance.Map<StockEntry>(model);
            User currentUser = this.GetCurrentUser(userId);
            entry.EntryDate = DateTime.Now;
            entry.Warehouse = warehouse;

            Record record = new Record()
            {
                AmountBefore = warehouse.CurrentStock,
                Distributor = currentUser
            };

            switch (entry.EntryType)
            {
                case "Input":
                    {
                        this.InputEntry(warehouse, entry.Amount);
                        break;
                    }
                case "Output":
                    {
                        this.OutputEntry(warehouse, entry.Amount);
                        break;
                    }
            }
            record.AmountAfter = warehouse.CurrentStock;
            entry.Record = record;

            this.Context.StockEntries.Add(entry);
            this.Context.SaveChanges();

        }

        public EntryAddViewModel GetEntryAddViewModel(EntryAddBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            EntryAddViewModel viewModel = Mapper.Instance.Map<EntryAddViewModel>(model);

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

        private void InputEntry(Warehouse warehouse, double amount)
        {
            if (warehouse.CurrentStock + amount > warehouse.Capacity)
            {
                throw new ArgumentException();
            }
            warehouse.CurrentStock += amount;
        }

        private void OutputEntry(Warehouse warehouse, double amount)
        {
            if (warehouse.CurrentStock - amount < 0)
            {
                throw new ArgumentException();
            }
            warehouse.CurrentStock -= amount;
        }
    }
}
