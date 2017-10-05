namespace WIT.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using WIT.App.Extensions;
    using WIT.Models.BindingModels.Record;
    using WIT.Models.ViewModels.Record;
    using WIT.Services.Interfaces;

    [RoutePrefix("records")]
    public class RecordsController : Controller
    {
        private IRecordService service;

        public RecordsController(IRecordService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Moderator,User")]
        [Route("addEntry")]
        public ActionResult AddEntry()
        {
            ViewBag.WarehouseList = this.service.GetWarehousesSelectListItems();
            return this.View(new EntryAddViewModel());
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin,Moderator,User")]
        [Route("addEntry")]
        public ActionResult AddEntry([Bind(Include = "EntryType,WhId,Amount")] EntryAddBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    string userId = this.User.Identity.GetUserId();
                    this.service.AddEntry(model, userId);
                    return this.RedirectToAction("All");

                }
                catch (ArgumentException e)
                {
                    throw new Exception(e.Message);
                }
            }

            ViewBag.WarehouseList = this.service.GetWarehousesSelectListItems();
            EntryAddViewModel viewModel = this.service.GetEntryAddViewModel(model);

            return this.View(viewModel);
        }
    }
}
