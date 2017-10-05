namespace WIT.App
{
    using System.Linq;
    using AutoMapper;
    using WIT.Models.BindingModels.Record;
    using WIT.Models.BindingModels.Warehouse;
    using WIT.Models.EntityModels;
    using WIT.Models.ViewModels.Record;
    using WIT.Models.ViewModels.User;
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

                #endregion

                #region Records bindings

                exp.CreateMap<Record, RecordListViewModel>()
                    .ForMember(model => model.Amount,
                        configurationExpression => configurationExpression.MapFrom(rec => rec.StockEntry.Amount))
                    .ForMember(model => model.EntryType,
                        configurationExpression => configurationExpression.MapFrom(rec => rec.StockEntry.EntryType))
                    .ForMember(model => model.Date,
                        configurationExpression => configurationExpression.MapFrom(rec => rec.StockEntry.EntryDate))
                    .ForMember(model => model.WarehouseName,
                        configurationExpression =>
                            configurationExpression.MapFrom(rec => rec.StockEntry.Warehouse.Name))
                    .ForMember(model => model.Company,
                        configurationExpression => configurationExpression.MapFrom(rec => rec.Distributor.CompanyName));

                exp.CreateMap<EntryAddBindingModel, StockEntry>();

                exp.CreateMap<EntryAddBindingModel, EntryAddViewModel>();

                #endregion


                #region User bindings

                exp.CreateMap<User, UserListViewModel>()
                    .ForMember(model => model.Comments,
                        configurationExpression => configurationExpression.MapFrom(user =>
                            user.Comments.Select(c => c.Content)))
                    .ForMember(model => model.Roles, configurationExpression =>
                        configurationExpression.Ignore());

                #endregion
            });
        }
    }
}
