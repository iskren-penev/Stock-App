namespace WIT.Data.Interfaces
{
    using System.Data.Entity;
    using WIT.Models.EntityModels;

    public interface IWitContext
    {
        IDbSet<User> Users { get; }

        DbSet<Warehouse> Warehouses { get; }

        DbSet<StockEntry> StockEntries { get; }

        DbSet<Record> Records { get; }

        DbSet<Comment> Comments { get; }

        int SaveChanges();
    }
}
