namespace Stock.Services.Implementations
{
    using Stock.Data.Interfaces;
    using Stock.Models.EntityModels;
    using Stock.Services.Interfaces;
    public abstract class Service : IService
    {
        protected Service(IStockContext context)
        {
            this.Context = context;
        }

        public IStockContext Context { get; set; }


        public User GetCurrentUser(string userId)
        {
            return this.Context.Users.Find(userId);
        }
    }
}
