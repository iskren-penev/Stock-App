namespace Stock.App.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using PagedList;
    using Stock.App.Extensions;
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
            return this.View();
        }
    }
}
