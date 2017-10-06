namespace WIT.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using WIT.Data.Interfaces;
    using WIT.Models.EntityModels;
    using WIT.Services.Interfaces;

    public abstract class Service : IService
    {
        protected Service(IWitContext context)
        {
            this.Context = context;
        }

        public IWitContext Context { get; set; }


        public User GetUserById(string userId)
        {
            return this.Context.Users.Find(userId);
        }

        public IEnumerable<Warehouse> GetWarehouses()
        {
            IEnumerable<Warehouse> warehouses = this.Context.Warehouses;

            return warehouses;
        }

        public Warehouse GetWarehouseById(int id)
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
