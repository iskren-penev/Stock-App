namespace WIT.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using WIT.Data.Interfaces;
    using WIT.Models.EntityModels;
    using WIT.Models.ViewModels.User;
    using WIT.Services.Interfaces;

    public class UserService: Service, IUserService
    {
        public UserService(IWitContext context) : base(context)
        {
        }

        public List<UserListViewModel> GetAllUsers()
        {
            List<User> users = this.Context.Users.ToList();
            List<UserListViewModel> viewModels =
                Mapper.Instance.Map<List<User>, List<UserListViewModel>>(users);

            return viewModels;
        }

        public List<UserListViewModel> SearchUsers(string search)
        {
            List<UserListViewModel> viewModels =this.GetAllUsers();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(vm =>
                        vm.Email.ToLower().Contains(search)
                        || vm.CompanyName.ToLower().Contains(search))
                    .ToList();
            }

            return viewModels;
        }

        public void SetRoleNameForModel(UserListViewModel model, List<string> roleNames)
        {
            foreach (string role in roleNames)
            {
                model.Roles.Add(role);
            }
        }
    }
}
