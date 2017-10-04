namespace Stock.App
{
    using AutoMapper;
    using Stock.Models.BindingModels.Warehouse;
    using Stock.Models.EntityModels;
    using Stock.Models.ViewModels.Warehouse;

    public static class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(exp =>
            {
                #region Warehouse bindings

                exp.CreateMap<Warehouse, WarehouseListViewModel>();

                exp.CreateMap<Warehouse, WarehouseEditViewModel>();

                exp.CreateMap<Warehouse, WarehouseDetailedViewModel>();

                exp.CreateMap<WarehouseAddBindingModel, Warehouse>();

                exp.CreateMap<WarehouseAddBindingModel, WarehouseAddViewModel>();

                #endregion
            });
        }
    }
}
