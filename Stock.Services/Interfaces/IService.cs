namespace Stock.Services.Interfaces
{
    using Stock.Data.Interfaces;
    using Stock.Models.EntityModels;

    public interface IService
    {
        IStockContext Context { get; set; }

        User GetCurrentUser(string userId);
    }
}
