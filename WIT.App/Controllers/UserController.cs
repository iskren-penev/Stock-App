namespace WIT.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using WIT.App.Extensions;
    using WIT.Models.BindingModels.User;
    using WIT.Models.ViewModels.User;
    using WIT.Services.Interfaces;

    [RoutePrefix("users")]
    public class UserController : Controller
    {
        private IUserService service;
        private ApplicationUserManager accManager;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        public UserController(ApplicationUserManager userManager)
        {
            Manager = userManager;
        }

        private ApplicationUserManager Manager
        {
            get { return this.accManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            set { this.accManager = value; }
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [Route]
        public ActionResult All()
        {
            List<UserListViewModel> viewModels = this.service.GetAllUsers();
            this.AddRolesToViewModels(viewModels);

            return this.View(viewModels);
        }

        [HttpGet]
        public PartialViewResult Display(string search)
        {
            List<UserListViewModel> viewModels = this.service.SearchUsers(search);
            this.AddRolesToViewModels(viewModels);

            return this.PartialView("_DisplayUsers", viewModels);
        }

        [Route("makemoderator")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult MakeModerator(string userId)
        {
            this.Manager.AddToRole(userId, "Moderator");
            return this.RedirectToAction("All");

        }

        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [Route("addComment")]
        public ActionResult AddComment()
        {
            ViewBag.UserList = this.service.GetUserSelectListItems();
            return this.View(new CommentAddViewModel());
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [Route("addComment")]
        public ActionResult AddComment([Bind(Include = "UserId,Content")] CommentAddBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddComment(model);
                return this.RedirectToAction("All");
            }

            ViewBag.UserList = this.service.GetUserSelectListItems();
            CommentAddViewModel viewModel = this.service.GetCommentAddViewModel(model);

            return this.View(viewModel);
        }

        private void AddRolesToViewModels(List<UserListViewModel> models)
        {
            foreach (var model in models)
            {
                List<string> roles = this.Manager.GetRoles(model.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }
        }
    }
}