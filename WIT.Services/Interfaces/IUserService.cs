namespace WIT.Services.Interfaces
{
    using System.Collections.Generic;
    using WIT.Models.ViewModels.User;

    public interface IUserService : IService
    {
        List<UserListViewModel> GetAllUsers();

        List<UserListViewModel> SearchUsers(string search);

        void SetRoleNameForModel(UserListViewModel model, List<string> roleNames);
    }
}
