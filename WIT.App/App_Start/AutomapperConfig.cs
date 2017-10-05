namespace WIT.App
{
    using AutoMapper;
    using WIT.Models.BindingModels.Record;
    using WIT.Models.BindingModels.Warehouse;
    using WIT.Models.EntityModels;
    using WIT.Models.ViewModels.Record;
    using WIT.Models.ViewModels.Warehouse;

    public static class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(exp =>
            {
                #region Warehouse bindings

                exp.CreateMap<Warehouse, WarehouseListViewModel>();

                exp.CreateMap<Warehouse, WarehouseEditViewModel>();

                exp.CreateMap<WarehouseAddBindingModel, Warehouse>();

                exp.CreateMap<WarehouseAddBindingModel, WarehouseAddViewModel>();

                exp.CreateMap<StockEntry, EntryListViewModel>();

                exp.CreateMap<EntryAddBindingModel, StockEntry>();

                exp.CreateMap<EntryAddBindingModel, EntryAddViewModel>();

                #endregion
            });
        }
    }
}
