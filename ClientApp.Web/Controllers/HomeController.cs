namespace ClientApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ClientApp.Services;
    using ClientApp.ViewModels;
    using System.Configuration;
    using ClientApp.Services.Contracts;
    using ClientApp.Services.Exceptions;
    using System.Net;
    using ClientApp.Database;
    using ClientApp.Web.Infrastructure;

    public class HomeController : Controller
    {
        private ISubscribeService subscribeService;
        private ICallBackService callBackService;
        private ArrivalsService arrivalService;

        // Poor man's IoC. Replace with Dependency container like Ninject.
        public HomeController()
            : this(new SubscribeService(), new CallBackService(), new ArrivalsService())
        {
        }
        public HomeController(ISubscribeService subscribeService, ICallBackService callBackService, ArrivalsService arrivalService)
        {
            this.subscribeService = subscribeService;
            this.callBackService = callBackService;
            this.arrivalService = arrivalService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Arrivals(int page)
        {
            var arrivals = this.arrivalService.GetEmployeeArrivalsByPage(page, 15);
            ViewBag.page = page;
            return View(arrivals);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult ArrivalsData(int page)
        {
            var db = new ApplicationDbContext();
            var arrivals = this.arrivalService.GetEmployeeArrivalsByPage(page, 15);
            return PartialView("EmployeeArrivalsPartial", arrivals);
        }

        [HttpGet]
        public ActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Subscribe(SubscribeModel model)
        {
            var webServiceUri = ConfigurationManager.AppSettings["WebServiceUri"];
            var clientCallBackUri = ConfigurationManager.AppSettings["ClientCallBackUri"];

            try
            {
                await this.subscribeService.Subscribe(model.Date, webServiceUri, clientCallBackUri);
            }
            catch (Exception)
            {
                // Handle exception thrown by service...
                throw new ApplicationException("Error while subscribing...");
            }

            return RedirectToAction("Arrivals", new { page = 0 });
        }


        [HttpPost]
        public async Task<ActionResult> CallBack(IEnumerable<EmployeeArrivalViewModel> arrivals)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var requestTocken = this.HttpContext.Request.Headers["X-Fourth-Token"];
                await this.callBackService.HandleEmployeeArrivals(requestTocken, arrivals);
            }
            catch (InvalidTokenException)
            {
                // Handle Invalid Token Exception...
            }

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}