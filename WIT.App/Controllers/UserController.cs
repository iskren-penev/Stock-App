namespace WIT.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using WIT.App.Extensions;
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

            return this.PartialView("_DisplayUsers");
        }

        [Route("makemoderator")]
        public ActionResult MakeModerator(string userId)
        {
            this.Manager.AddToRole(userId, "Moderator");
            return this.RedirectToAction("All");

        }

        [HttpGet]
        [Route("addComment")]

        private void AddRolesToViewModels(List<UserListViewModel> models)
        {
            foreach (var model in models)
            {
                var roles = this.Manager.GetRoles(model.Id).ToList();
                this.service.SetRoleNameForModel(model, roles);
            }
        }
    }
}