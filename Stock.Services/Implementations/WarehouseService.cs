namespace Stock.Services.Implementations
{
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
            throw new System.NotImplementedException();
        }

        public void EditWarehouse(WarehouseEditBindingModel model)
        {
            throw new System.NotImplementedException();
        }

        public WarehouseDetailedViewModel GetDaDetailedViewModel(int id)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Warehouse> GetWarehouses()
        {
            IEnumerable<Warehouse> warehouses = this.Context.Warehouses;
            return warehouses;
        }
    }
}
