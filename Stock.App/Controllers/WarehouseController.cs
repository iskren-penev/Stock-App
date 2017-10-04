namespace Stock.App.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;
    using PagedList;
    using Stock.App.Extensions;
    using Stock.Models.BindingModels.Warehouse;
    using Stock.Models.ViewModels.Warehouse;
    using Stock.Services.Interfaces;

    [RoutePrefix("warehouses")]
    public class WarehouseController : Controller
    {
        private IWarehouseService service;

        public WarehouseController(IWarehouseService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route]
        public ActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        public PartialViewResult Display(string search, int? page)
        {
            this.ViewBag.SearchKeyword = search;

            IEnumerable<WarehouseListViewModel> viewModels = this.service.GetListViewModelsSearch(search);

            return this.PartialView("_DisplayWarehouses", viewModels.ToPagedList(page ?? 1, 10));
        }


        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [Route("add")]
        public ActionResult Add(WarehouseAddViewModel model)
        {
            return this.View(new WarehouseAddViewModel());
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [Route("add")]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Name,Address,Capacity")] WarehouseAddBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this.User == null)
                {
                    return this.RedirectToAction("All");
                }
                this.service.AddWarehouse(model);
                return this.RedirectToAction("All");

            }
            WarehouseAddViewModel viewModel = this.service.GetAddViewModel(model);

            return this.View(viewModel);
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [Route("edit/{id:int:min{1}}")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarehouseEditViewModel viewModel = this.service.GetEditViewModel(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin,Moderator")]
        [Route("edit/{id:int:min{1}}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Capacity")] WarehouseEditBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditWarehouse(model);
                return this.RedirectToAction("Details", new {id = model.Id});
            }
            WarehouseEditViewModel viewModel = this.service.GetEditViewModel(model.Id);

            return this.View(viewModel);
        }


        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Moderator,User")]
        [Route("{id:int:min(1)}")]
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarehouseDetailedViewModel viewModel = this.service.GetDetailedViewModel(id);

            return this.View(viewModel);
        }

    }
}
