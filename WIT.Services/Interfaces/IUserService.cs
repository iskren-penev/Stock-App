namespace WIT.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using WIT.Models.BindingModels.User;
    using WIT.Models.ViewModels.User;

    public interface IUserService : IService
    {
        List<UserListViewModel> GetAllUsers();

        List<UserListViewModel> SearchUsers(string search);

        void SetRoleNameForModel(UserListViewModel model, List<string> roleNames);

        IEnumerable<SelectListItem> GetUserSelectListItems();

        void AddComment(CommentAddBindingModel model);

        CommentAddViewModel GetCommentAddViewModel(CommentAddBindingModel model);
    }
}
