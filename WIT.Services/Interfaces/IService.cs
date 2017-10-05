namespace WIT.Services.Interfaces
{
    using WIT.Data.Interfaces;
    using WIT.Models.EntityModels;

    public interface IService
    {
        IWitContext Context { get; set; }

        User GetCurrentUser(string userId);
    }
}
