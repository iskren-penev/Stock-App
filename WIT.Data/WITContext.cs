namespace WIT.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WIT.Data.Interfaces;
    using WIT.Data.Migrations;
    using WIT.Models.EntityModels;

    public class WitContext : IdentityDbContext<User>, IWitContext
    {
        public WitContext() : base("WitContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WitContext, Configuration>());

        }

        public static WitContext Create()
        {
            return new WitContext();
        }

        public virtual DbSet<Warehouse> Warehouses { get; set; }

        public virtual DbSet<StockEntry> StockEntries { get; set; }

        public virtual DbSet<Record> Records { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }
    }

}