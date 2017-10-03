namespace Stock.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Stock.Data.Interfaces;
    using Stock.Data.Migrations;
    using Stock.Models.EntityModels;

    public class StockContext : IdentityDbContext<User>, IStockContext
    {
        public StockContext() : base("StockContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StockContext, Configuration>());

        }

        public static StockContext Create()
        {
            return new StockContext();
        }

        public virtual DbSet<Warehouse> Warehouses { get; set; }

        public virtual DbSet<StockEntry> StockEntries { get; set; }

        public virtual DbSet<Record> Records { get; set; }
    }

}