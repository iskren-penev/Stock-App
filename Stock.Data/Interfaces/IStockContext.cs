namespace Stock.Data.Interfaces
{
    using System.Data.Entity;
    using Stock.Models.EntityModels;

    public interface IStockContext
    {
        IDbSet<User> Users { get; }

        DbSet<Warehouse> Warehouses { get; }

        DbSet<StockEntry> StockEntries { get; }

        DbSet<Record> Records { get; }

        int SaveChanges();
    }
}
