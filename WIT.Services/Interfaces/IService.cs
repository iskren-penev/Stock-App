namespace WIT.Services.Interfaces
{
    using System.Collections.Generic;
    using WIT.Data.Interfaces;
    using WIT.Models.EntityModels;

    public interface IService
    {
        IWitContext Context { get; set; }

        User GetUserById(string userId);

        IEnumerable<Warehouse> GetWarehouses();

        Warehouse GetWarehouseById(int id);
    }
}
