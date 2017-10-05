namespace WIT.Services.Implementations
{
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


        public User GetCurrentUser(string userId)
        {
            return this.Context.Users.Find(userId);
        }
    }
}
