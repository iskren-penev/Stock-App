namespace WIT.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using WIT.Data.Interfaces;
    using WIT.Models.BindingModels.Record;
    using WIT.Models.EntityModels;
    using WIT.Models.ViewModels.Record;
    using WIT.Services.Interfaces;

    public class RecordService : Service, IRecordService
    {
        public RecordService(IWitContext context) : base(context)
        {
        }

        public void AddEntry(EntryAddBindingModel model, string userId)
        {
            Warehouse warehouse = this.GetWarehouseById(int.Parse(model.WhId));
            StockEntry entry = Mapper.Instance.Map<StockEntry>(model);
            User currentUser = this.GetCurrentUser(userId);
            entry.EntryDate = DateTime.Now;

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
            warehouse.Entries.Add(entry);
            this.Context.StockEntries.Add(entry);
            this.Context.SaveChanges();

            record.StockEntryId = entry.Id;
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

        public IEnumerable<SelectListItem> GetWarehousesSelectListItems()
        {
            IEnumerable<SelectListItem> selectListItems = this.GetWarehouses()
                .Select(w => new SelectListItem()
                {
                    Text = w.Name,
                    Value = w.Id.ToString()
                });

            return selectListItems;
        }

        public List<RecordListViewModel> GetRecordListViewModels()
        {
            List<Record> records = this.GetAllRecords().ToList();
            List<RecordListViewModel> viewModels =
                Mapper.Instance.Map<List<RecordListViewModel>>(records);

            return viewModels;
        }

        public List<RecordListViewModel> GetRecordListViewModelsSearch(string search)
        {
            List<RecordListViewModel> viewModels = this.GetRecordListViewModels();
            if (!string.IsNullOrEmpty(search))
            {
                viewModels = viewModels.Where(vm =>
                        vm.Company.ToLower().Contains(search.ToLower())
                        || vm.EntryType.ToLower().Contains(search.ToLower())
                        || vm.WarehouseName.ToLower().Contains(search.ToLower()))
                    .ToList();
            }
            return viewModels;
        }

        private void InputEntry(Warehouse warehouse, double amount)
        {
            if (warehouse.CurrentStock + amount > warehouse.Capacity)
            {
                throw new ArgumentException("The amount of stock in the warehouse cannot exceed the capacity!");
            }
            warehouse.CurrentStock += amount;
        }

        private void OutputEntry(Warehouse warehouse, double amount)
        {
            if (warehouse.CurrentStock - amount < 0)
            {
                throw new ArgumentException("The amount of stock in the warehouse cannot be less than 0!");
            }
            warehouse.CurrentStock -= amount;
        }

        private IEnumerable<Record> GetAllRecords()
        {
            IEnumerable<Record> records = this.Context.Records;

            return records;
        }
    }
}
