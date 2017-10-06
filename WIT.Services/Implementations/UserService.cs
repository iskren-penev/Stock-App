namespace WIT.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using WIT.Data.Interfaces;
    using WIT.Models.BindingModels.User;
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
            List<User> users =this.GetUsers().ToList();
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

        public IEnumerable<SelectListItem> GetUserSelectListItems()
        {
            IEnumerable<SelectListItem> selectListItems = this.GetUsers()
                .Select(u => new SelectListItem()
                {
                    Text = u.CompanyName,
                    Value = u.Id
                });

            return selectListItems;
        }

        public void AddComment(CommentAddBindingModel model)
        {
            User user = this.GetUserById(model.UserId);
            Comment comment = Mapper.Instance
                .Map<CommentAddBindingModel, Comment>(model);
            comment.User = user;

            this.Context.Comments.Add(comment);
            this.Context.SaveChanges();
        }

        public CommentAddViewModel GetCommentAddViewModel(CommentAddBindingModel model)
        {
            CommentAddViewModel viewModel = Mapper.Instance
                .Map<CommentAddBindingModel, CommentAddViewModel>(model);

            return viewModel;
        }

        private IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = this.Context.Users;

            return users;
        }
    }
}
